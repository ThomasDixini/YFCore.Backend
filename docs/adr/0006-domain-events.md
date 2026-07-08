# ADR-0006 — Domain Events

## Status

Aceito

## Contexto

Determinadas ações de negócio precisam disparar comportamentos adicionais sem aumentar o acoplamento entre entidades.

## Decisão

Utilizar Domain Events publicados através do MediatR após a confirmação da Unit of Work.

## Consequências

### Positivas

- Baixo acoplamento
- Melhor extensibilidade
- Regras independentes

### Negativas

- Fluxo de execução mais complexo
- Necessidade de rastrear eventos publicados

## Alternativas consideradas

Chamadas diretas entre serviços, descartadas por aumentar o acoplamento.