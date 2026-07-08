# Autenticação

A autenticação utiliza JWT Bearer.

Fluxo:

```mermaid
sequenceDiagram

User->>API: Login

API->>Identity: Validate Credentials

Identity-->>API: OK

API->>JWT: Generate Token

JWT-->>User: Access Token
```

Após autenticado, o usuário envia:

Authorization: Bearer {token}

Todas as requisições protegidas passam pelo middleware JWT antes de chegar aos Controllers.
