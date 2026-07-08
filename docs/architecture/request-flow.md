# Fluxo de uma Requisição

```mermaid
sequenceDiagram

Client->>Controller: POST /appointments

Controller->>Mediator: Send(Command)

Mediator->>Handler: Execute

Handler->>Repository: Save

Repository->>DbContext: Add()

DbContext-->>Repository: Success

Repository-->>Handler

Handler-->>Mediator

Mediator-->>Controller

Controller-->>Client: 201 Created
```
