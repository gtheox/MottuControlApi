# 🚀 MottuControlApi

API RESTful desenvolvida com ASP.NET Core 8 para o gerenciamento inteligente de motos, sensores, status de monitoramento e pátios de estacionamento da empresa Mottu.

---

## 📚 Sumário

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Modelagem do Banco](#modelagem-do-banco)
- [Instalação](#instalação)
- [Execução](#execução)
- [Rotas da API](#rotas-da-api)
- [Desenvolvedores](#desenvolvedores)

---

## 📖 Descrição

Esta aplicação foi construída para otimizar o monitoramento das motos nas filiais da empresa Mottu. Com ela, é possível:

- Cadastrar e consultar pátios
- Controlar as motos disponíveis, alugadas e em manutenção
- Associar sensores IoT às motos
- Registrar e consultar o histórico de status
- Armazenar imagens dos pátios para apoio visual

---

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Oracle Database 21c
- Entity Framework Core
- Swashbuckle (Swagger)
- REST Client (`.http` file para testes)

---

## 🗃️ Modelagem do Banco

**Tabelas principais:**

- `Patios`: local onde as motos estão estacionadas
- `Motos`: contém modelo, placa, status e pátio
- `Sensores`: sensores físicos vinculados à moto
- `ImagensPatio`: fotos registradas por câmeras
- `StatusMonitoramentos`: histórico do status das motos

---

## 💻 Instalação

### 1. Pré-requisitos

- .NET SDK 8 instalado
- Oracle Database local ou remoto configurado
- Ferramenta cliente como SQL Developer (opcional)

### 2. Clonar o projeto

````bash
git clone https://github.com/seu-usuario/MottuControlApi.git
cd MottuControlApi
````

### 3. Restaurar dependências

```bash
dotnet restore
````

### 4. Aplicar migrations no banco Oracle

```bash
dotnet ef database update
````

## ▶️ Execução

- Para rodar o projeto em ambiente de desenvolvimento:

```bash
dotnet run
````

Acesse a documentação interativa:

```bash
https://localhost:5001/swagger
````

## 📡 Rotas da API

### Você pode testar com o arquivo MottuControlApi.http ou via Swagger.

| Método | Endpoint                      | Descrição                          |
| ------ | ----------------------------- | ---------------------------------- |
| GET    | `/api/patio`                  | Listar todos os pátios             |
| GET    | `/api/moto/buscar?placa=XYZ`  | Buscar moto por placa              |
| POST   | `/api/sensor`                 | Cadastrar novo sensor              |
| GET    | `/api/imagem/patio/{patioId}` | Ver imagens de um pátio específico |
| POST   | `/api/statusmonitoramento`    | Registrar novo status da moto      |


## 👨‍💻 Desenvolvedores
| Nome                           | RM     | GitHub                                          |
| ------------------------------ | ------ | ----------------------------------------------- |
| Gabriel Teodoro Gonçalves Rosa | 555962 | [gtheox](https://github.com/gtheox)             |
| Luka Shibuya                   | 558123 | [lukashibuya](https://github.com/lukashibuya)   |
| Eduardo Giovannini             | 555030 | [DuGiovannini](https://github.com/DuGiovannini) |

