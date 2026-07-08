# Camadas

## API

A camada de apresentação.

Não contém regras de negócio.

Responsável por:

- Receber requisições
- Retornar respostas HTTP
- Autenticação
- Autorização

---

## Application

Orquestra os casos de uso.

Não conhece Entity Framework.

Não conhece SQL Server.

Seu papel é coordenar a execução do domínio.

---

## Domain

Contém toda regra de negócio.

Exemplo:

- Não permitir agendamento em horário ocupado
- Validar preços
- Validar estados da entidade

---

## Infrastructure

Implementa detalhes técnicos.

Exemplo:

- EF Core
- SQL Server
- JWT
- Persistência
- Repositórios
