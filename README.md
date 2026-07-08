# YFCore Backend

<p align="center">
  <img src="./docs/images/banner.png" alt="YFCore Banner" width="100%">
</p>

<p align="center">

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Architecture](https://img.shields.io/badge/Clean%20Architecture-DDD-blue)
![CQRS](https://img.shields.io/badge/CQRS-MediatR-success)
![Entity Framework](https://img.shields.io/badge/EF-Core-purple)
![JWT](https://img.shields.io/badge/Auth-JWT-orange)
![License](https://img.shields.io/badge/license-MIT-green)

</p>

> Backend desenvolvido em **ASP.NET Core** utilizando **Domain-Driven Design (DDD)**, **Clean Architecture**, **CQRS**, **Entity Framework Core** e **JWT Authentication**, com foco em escalabilidade, manutenção e boas práticas de desenvolvimento.

---

# Índice

- [Sobre](#sobre)
- [Principais Funcionalidades](#principais-funcionalidades)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Estrutura da Solução](#estrutura-da-solução)
- [Começando](#começando)
- [Autenticação](#autenticação)
- [Testes](#testes)
- [Documentação](#documentação)
- [Roadmap](#roadmap)
- [Contribuindo](#contribuindo)
- [Autor](#autor)

---

# Sobre

O **YFCore Backend** é uma API REST desenvolvida para gerenciamento de clínicas estéticas, seguindo princípios modernos de arquitetura de software.

O principal objetivo deste projeto foi aplicar conceitos utilizados em sistemas corporativos, priorizando:

- Separação de responsabilidades
- Escalabilidade
- Alta coesão
- Baixo acoplamento
- Facilidade de manutenção
- Testabilidade
- Evolução contínua

O projeto foi desenvolvido como forma de aprofundar conhecimentos em arquitetura de software e boas práticas no ecossistema .NET.

---

# Principais Funcionalidades

- Autenticação utilizando JWT
- Gerenciamento de usuários
- Gerenciamento de categorias
- Gerenciamento de tipos de procedimentos
- Agendamento de procedimentos
- Validações utilizando FluentValidation
- Separação de Commands e Queries (CQRS)
- Publicação de Domain Events
- Persistência utilizando Entity Framework Core
- Documentação interativa utilizando Scalar

---

# Arquitetura

O projeto segue os princípios da **Clean Architecture**.

```text
                Client
                   │
                   ▼
              ASP.NET API
                   │
                   ▼
             Application
                   │
                   ▼
               Domain
                   ▲
                   │
           Infrastructure
                   │
                   ▼
              SQL Server
```

## Princípios adotados

- Clean Architecture
- Domain-Driven Design
- SOLID
- Clean Code
- CQRS
- Repository Pattern
- Unit of Work
- Domain Events
- Dependency Injection

---

# Tecnologias

| Categoria | Tecnologias |
|-----------|-------------|
| Backend | ASP.NET Core (.NET 10) |
| Linguagem | C# |
| Banco de Dados | SQL Server |
| ORM | Entity Framework Core |
| Arquitetura | Clean Architecture + DDD |
| Mensageria Interna | MediatR |
| Validação | FluentValidation |
| Autenticação | JWT Bearer |
| Documentação | Scalar |
| Testes | xUnit, Moq, FluentAssertions |

---

# Estrutura da Solução

```text
src/
│
├── YFCore.Api
├── YFCore.Application
├── YFCore.Domain
└── YFCore.Infrastructure

tests/
│
└── YFCore.Tests.Unit

docs/
│
├── adr/
├── api/
├── architecture/
├── diagrams/
└── guides/
```

---

# Fluxo da Aplicação

```text
HTTP Request

↓

Controller

↓

Command / Query

↓

Handler

↓

Domain

↓

Repository

↓

Entity Framework Core

↓

SQL Server
```

---

# Começando

## Clonar o repositório

```bash
git clone https://github.com/ThomasDixini/YFCore.Backend.git
```

## Restaurar dependências

```bash
dotnet restore
```

## Aplicar as migrations

```bash
dotnet ef database update
```

## Executar

```bash
dotnet run --project src/YFCore.Api
```

---

# Autenticação

A API utiliza autenticação baseada em **JWT Bearer Token**.

Após autenticar-se, envie o token em todas as requisições protegidas:

```http
Authorization: Bearer {token}
```

---

# Documentação da API

A API possui documentação interativa utilizando **Scalar**.

Após iniciar a aplicação, acesse:

```text
/scalar
```

---

# Testes

O projeto possui testes unitários utilizando:

- xUnit
- Moq
- FluentAssertions

Executar todos os testes:

```bash
dotnet test
```

---

# Documentação

Toda a documentação do projeto encontra-se na pasta `docs`.

| Documento | Descrição |
|------------|-----------|
| Architecture | Arquitetura da aplicação |
| ADR | Architecture Decision Records |
| API | Documentação dos endpoints |
| Diagrams | Diagramas Mermaid |
| Guides | Guias de desenvolvimento |

---

# Destaques do Projeto

- Arquitetura em camadas
- Modelo de domínio rico
- Regras de negócio centralizadas
- Separação entre leitura e escrita (CQRS)
- Publicação de Domain Events
- Validação desacoplada
- Documentação completa
- Código organizado para evolução e manutenção

---

# Contribuindo

Contribuições são bem-vindas.

Antes de abrir um Pull Request:

- Execute todos os testes
- Siga os padrões de código
- Utilize Conventional Commits
- Atualize a documentação quando necessário

---

# Autor

**Thomás Dixini**

Desenvolvedor Backend | .NET | C# | ASP.NET Core

GitHub:
https://github.com/ThomasDixini

LinkedIn:
https://www.linkedin.com/in/thomasdixini/

---

# Licença

Este projeto está licenciado sob a licença MIT.
