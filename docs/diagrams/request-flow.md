# Fluxo da Requisição

```mermaid
sequenceDiagram

participant Client

participant Controller

participant Mediator

participant Handler

participant Domain

participant Repository

participant DbContext

participant SQL

Client->>Controller: HTTP Request

Controller->>Mediator: Send(Command)

Mediator->>Handler: Execute()

Handler->>Domain: Business Rules

Domain-->>Handler: Success

Handler->>Repository: Save()

Repository->>DbContext: Add/Update()

DbContext->>SQL: SaveChanges()

SQL-->>DbContext: OK

DbContext-->>Repository

Repository-->>Handler

Handler-->>Mediator

Mediator-->>Controller

Controller-->>Client: HTTP Response
```