# Clean Architecture

```mermaid
flowchart TB

Client["Client Application"]

API["API"]

Application["Application"]

Domain["Domain"]

Infrastructure["Infrastructure"]

Database[(SQL Server)]

Client --> API

API --> Application

Application --> Domain

Application --> Infrastructure

Infrastructure --> Database

Infrastructure -.implements.-> Domain
```

## Descrição

A arquitetura é organizada em camadas.

As regras de negócio permanecem no centro da aplicação (Domain), enquanto detalhes técnicos permanecem na Infrastructure.

As dependências sempre apontam para dentro.