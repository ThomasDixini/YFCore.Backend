# Dependências entre Projetos

```mermaid
graph LR

API

Application

Domain

Infrastructure

API --> Application

Application --> Domain

Infrastructure --> Domain

API --> Infrastructure
```

A camada Domain não possui dependências de outras camadas.

Essa organização reduz o acoplamento e facilita a evolução do sistema.