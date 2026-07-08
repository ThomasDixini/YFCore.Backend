# ADR-0007 — FluentValidation

## Status

Aceito

## Contexto

Os comandos precisam validar dados de entrada antes da execução das regras de negócio.

## Decisão

Utilizar FluentValidation para validar Commands na camada Application.

As regras de negócio continuam sendo responsabilidade do domínio.

## Consequências

### Positivas

- Validações centralizadas
- Código mais limpo
- Mensagens padronizadas

### Negativas

- Dependência adicional

## Alternativas consideradas

Validações manuais nos Handlers, descartadas por gerar repetição de código.