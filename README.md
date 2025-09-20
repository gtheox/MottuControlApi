# 🚀 MottuControlApi

API RESTful desenvolvida com ASP.NET Core para o gerenciamento inteligente de motos, sensores, status de monitoramento e pátios de estacionamento da empresa Mottu. Este projeto foi construído seguindo as melhores práticas de desenvolvimento, incluindo arquitetura em camadas, injeção de dependência e princípios SOLID.

---

## 📚 Sumário

- [Descrição](#-descrição)
- [✨ Features & Boas Práticas](#-features--boas-práticas)
- [🏛️ Arquitetura](#️-arquitetura)
- [🛠️ Tecnologias Utilizadas](#️-tecnologias-utilizadas)
- [💻 Instalação e Execução](#-instalação-e-execução)
- [📡 Exemplos de Uso da API](#-exemplos-de-uso-da-api)
- [✅ Testes](#-testes)
- [👨‍💻 Desenvolvedores](#-desenvolvedores)

---

## 📖 Descrição

Esta aplicação foi construída para otimizar o monitoramento das motos nas filiais da empresa Mottu. Com ela, é possível:

- Cadastrar e consultar pátios de forma paginada.
- Controlar as motos disponíveis, alugadas e em manutenção.
- Associar sensores IoT às motos.
- Registrar e consultar o histórico de status de cada moto.

---

## ✨ Features & Boas Práticas

- **Arquitetura em Camadas:** Separação clara de responsabilidades entre Apresentação (Controllers), Lógica de Negócio (Services) e Acesso a Dados (Repositories).
- **Injeção de Dependência e Interfaces:** Uso extensivo de interfaces (`IMotoService`, `IMotoRepository`) para desacoplar as camadas, facilitando a manutenção e a testabilidade (princípios SOLID).
- **Paginação:** Todos os endpoints de listagem (`GET`) retornam dados paginados para otimizar a performance. Os metadados da paginação são enviados no cabeçalho `X-Pagination` da resposta.
- **Documentação OpenAPI (Swagger):** Todos os endpoints são 100% documentados, incluindo schemas, exemplos e descrições detalhadas de cada parâmetro e resposta.
- **DTOs (Data Transfer Objects):** A API utiliza DTOs para definir contratos claros e seguros com os clientes, evitando a exposição direta dos modelos do banco de dados.

---

## 🏛️ Arquitetura

Para este projeto, foi adotada a **Arquitetura em Camadas (N-Tier Architecture)**, uma abordagem robusta e amplamente utilizada no mercado que promove a separação de responsabilidades.

1.  **Camada de Apresentação (Controllers):** Responsável por gerenciar as requisições HTTP (rotas), validar os dados de entrada (DTOs) e formatar as respostas. Ela não contém nenhuma regra de negócio.
2.  **Camada de Serviço (Services):** Onde reside a lógica de negócio da aplicação. Ela orquestra as operações, chamando a camada de repositório para manipular os dados e aplicando as regras necessárias.
3.  **Camada de Repositório (Repositories):** A única camada com a responsabilidade de se comunicar diretamente com o banco de dados através do Entity Framework Core. Ela abstrai a lógica de acesso aos dados, fornecendo métodos claros para consulta e persistência.

Essa arquitetura garante um baixo acoplamento entre os componentes, tornando o sistema mais fácil de testar, manter e evoluir.

---

## 🛠️ Tecnologias Utilizadas

- .NET 8 (ou superior)
- ASP.NET Core Web API
- Oracle Database
- Entity Framework Core
- Swashbuckle (Swagger) para documentação OpenAPI

---

## 💻 Instalação e Execução

### 1. Pré-requisitos

- .NET SDK 8 (ou superior) instalado.
- Acesso a uma instância do Oracle Database.

### 2. Clonar o projeto

```bash
git clone [https://github.com/seu-usuario/MottuControlApi.git](https://github.com/seu-usuario/MottuControlApi.git)
cd MottuControlApi
```

### 3. Configurar a Conexão

Abra o arquivo appsettings.json e altere a OracleConnection para a sua string de conexão com o banco.

### 4. Restaurar Dependências e Aplicar Migrations

```bash
dotnet restore
dotnet ef database update
```

### 5. Executar a Aplicação

```bash
dotnet run
```

A API estará disponível em https://localhost:7010 (HTTPS) e http://localhost:5012 (HTTP).

Acesse a documentação interativa do Swagger em: https://localhost:7010/swagger

---

## 📡 Exemplos de Uso da API

<details>
<summary><strong>GET /api/motos - Listar motos com paginação</strong></summary>

**Comando cURL:**

```bash
curl -X GET "https://localhost:7010/api/motos?pageNumber=1&pageSize=3" -k
```

**Resposta:**

Cabeçalho de Resposta `X-Pagination`:

```json
{ "TotalCount": 5, "PageSize": 3, "CurrentPage": 1, "TotalPages": 2, "HasNext": true, "HasPrevious": false }
```

Corpo da Resposta:

```json
[
  {
    "id": 1,
    "modelo": "Honda Biz 110i",
    "placa": "ABC1D23",
    "status": "Disponível",
    "patioId": 1,
    "nomePatio": "Pátio Central",
    "sensores": [...],
    "statusMonitoramentos": [...]
  }
]
```

</details>

<details>
<summary><strong>POST /api/motos - Criar uma nova moto</strong></summary>

**Comando cURL:**

```bash
curl -X POST "https://localhost:7010/api/motos" -k -H "Content-Type: application/json" -d '{
  "modelo": "Yamaha Fazer 250",
  "placa": "FAZ3R25",
  "status": "Disponível",
  "patioId": 1
}'
```

**Resposta (201 Created):**

```json
{
  "id": 6,
  "modelo": "Yamaha Fazer 250",
  "placa": "FAZ3R25",
  "status": "Disponível",
  "patioId": 1,
  "nomePatio": "Pátio Central",
  "sensores": [],
  "statusMonitoramentos": []
}
```

</details>

<details>
<summary><strong>PUT /api/motos/{id} - Atualizar uma moto</strong></summary>

**Comando cURL:**

```bash
curl -X PUT "https://localhost:7010/api/motos/1" -k -H "Content-Type: application/json" -d '{
  "modelo": "Honda Biz 110i",
  "status": "Manutenção",
  "patioId": 1
}'
```

**Resposta (200 OK):**

```json
{
  "id": 1,
  "modelo": "Honda Biz 110i",
  "placa": "ABC1D23",
  "status": "Manutenção",
  "patioId": 1
}
```

</details>

---

## ✅ Testes

O projeto foi estruturado com interfaces e injeção de dependência, estando totalmente preparado para a implementação de testes unitários e de integração. Para executar os testes (após a criação de um projeto de testes):

```bash
dotnet test
```

---

## 👨‍💻 Desenvolvedores

| Nome                           | RM     | GitHub                                          |
| ------------------------------ | ------ | ----------------------------------------------- |
| Gabriel Teodoro Gonçalves Rosa | 555962 | [gtheox](https://github.com/gtheox)             |
| Luka Shibuya                   | 558123 | [lukashibuya](https://github.com/lukashibuya)   |
| Eduardo Giovannini             | 555030 | [DuGiovannini](https://github.com/DuGiovannini) |
