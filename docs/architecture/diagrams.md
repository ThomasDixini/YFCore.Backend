# Diagramas

## Clean Architecture

```mermaid
flowchart TD

API --> Application

Application --> Domain

Infrastructure --> Domain

Application --> Infrastructure
```

---

## CQRS

```mermaid
flowchart LR

Controller

Command

Handler

Repository

Database

Controller --> Command

Command --> Handler

Handler --> Repository

Repository --> Database
```

---

## Domain Events

```mermaid
flowchart TD

Entity

Raise Event

UnitOfWork

Mediator

Handler

Entity --> Raise Event

Raise Event --> UnitOfWork

UnitOfWork --> Mediator

Mediator --> Handler
```
