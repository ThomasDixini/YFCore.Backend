# ADR-0002 — Domain-Driven Design

## Status

Aceito

## Contexto

A aplicação possui regras de negócio que evoluem constantemente e precisam permanecer isoladas da infraestrutura.

## Decisão

Centralizar toda regra de negócio na camada Domain utilizando os conceitos de Domain-Driven Design.

Foram adotados:

- Entidades
- Agregados
- Value Objects
- Domain Services
- Domain Events
- Repositórios como abstração

## Consequências

### Positivas

- Modelo rico
- Regras centralizadas
- Facilidade de evolução
- Maior expressividade do domínio

### Negativas

- Curva de aprendizado
- Maior quantidade de código

## Alternativas consideradas

Modelo anêmico, onde toda lógica fica na camada Application. Essa abordagem foi descartada para evitar dispersão das regras de negócio.