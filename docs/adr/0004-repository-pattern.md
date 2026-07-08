# ADR-0004 — Repository Pattern

## Status

Aceito

## Contexto

O domínio não deve conhecer detalhes de persistência.

## Decisão

Definir interfaces de repositório na camada Domain e implementá-las na Infrastructure utilizando Entity Framework Core.

## Consequências

### Positivas

- Isolamento da persistência
- Facilidade para testes
- Menor acoplamento

### Negativas

- Camada adicional de abstração

## Alternativas consideradas

Acesso direto ao DbContext pela camada Application, descartado por violar os princípios da arquitetura adotada.