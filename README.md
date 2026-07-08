# YFCore Backend

> Backend desenvolvido em ASP.NET Core utilizando Domain-Driven Design (DDD), Clean Architecture, CQRS e Entity Framework Core.

![.NET](https://img.shields.io/badge/.NET-10-purple)
![C#](https://img.shields.io/badge/C%23-13-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

# Sobre

O YFCore Backend é uma API REST desenvolvida para gerenciamento de clínicas estéticas, permitindo controlar usuários, procedimentos, agendamentos e demais recursos da aplicação.

O projeto foi desenvolvido seguindo princípios modernos de arquitetura de software visando:

- Escalabilidade
- Baixo acoplamento
- Alta coesão
- Facilidade de testes
- Evolução contínua

---

# Objetivos

- Demonstrar boas práticas em .NET
- Aplicar Domain-Driven Design
- Aplicar Clean Architecture
- Utilizar CQRS com MediatR
- Centralizar regras de negócio no domínio
- Facilitar manutenção e evolução

---

# Tecnologias

- ASP.NET Core
- .NET 10
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- JWT Authentication
- Docker
- Swagger
- xUnit
- Moq

---

# Arquitetura

O projeto segue o modelo de Clean Architecture.

```

API

↓

Application

↓

Domain

↑

Infrastructure

```

Cada camada possui responsabilidades bem definidas.

| Projeto | Responsabilidade |
|----------|------------------|
| API | Endpoints REST |
| Application | Casos de uso |
| Domain | Regras de negócio |
| Infrastructure | Persistência |

---

# Estrutura

```

src/

YFCore.Api

YFCore.Application

YFCore.Domain

YFCore.Infrastructure

tests/

docs/

```

---

# Princípios adotados

- SOLID
- Clean Code
- Domain-Driven Design
- CQRS
- Repository Pattern
- Unit of Work
- Domain Events
- Dependency Injection

---

# Fluxo da aplicação

```text
HTTP Request

↓

Controller

↓

Command / Query

↓

Handler

↓

Repository

↓

Entity Framework Core

↓

SQL Server
```

---

# CQRS

O projeto separa operações de leitura e escrita.

## Commands

Responsáveis por alterar o estado da aplicação.

Exemplo:

- Create Procedure
- Update Procedure
- Delete Procedure

## Queries

Responsáveis apenas por leitura.

Exemplo:

- Get Procedure
- Get Procedures

---

# Domain Events

Eventos de domínio são utilizados para desacoplar regras de negócio.

Exemplo:

AppointmentScheduled

↓

Notification Handler

↓

Envio de notificações

---

# Validações

Todas as validações são realizadas utilizando FluentValidation.

Cada Command possui seu respectivo Validator.

---

# Autenticação

A autenticação é realizada utilizando JWT.

Fluxo:

Login

↓

Token JWT

↓

Bearer Authentication

↓

API

---

# Banco de Dados

Persistência realizada utilizando Entity Framework Core.

O acesso aos dados é abstraído através de Repositories.

---

# Testes

O projeto possui testes unitários utilizando:

- xUnit
- Moq
- FluentAssertions

---

# Executando localmente

## Clonar

```bash
git clone https://github.com/SEU_USUARIO/YFCore.Backend.git
```

## Restaurar

```bash
dotnet restore
```

## Executar

```bash
dotnet run
```

---

# Executando os testes

```bash
dotnet test
```

---

# Migrations

Criar migration

```bash
dotnet ef migrations add InitialCreate
```

Aplicar

```bash
dotnet ef database update
```

---

# Documentação

Toda documentação encontra-se na pasta:

```
docs/
```

Incluindo:

- Arquitetura
- ADRs
- Diagramas
- API

---

# Roadmap

- [ ] Refresh Token
- [ ] Cache Redis
- [ ] Health Checks
- [ ] Observabilidade
- [ ] Mensageria
- [ ] Testes de Integração

---

# Licença

MIT