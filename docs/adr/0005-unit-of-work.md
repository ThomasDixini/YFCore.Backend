# ADR-0005 — Unit of Work

## Status

Aceito

## Contexto

Uma operação pode alterar múltiplos agregados e publicar eventos de domínio.

## Decisão

Centralizar a confirmação das alterações em uma implementação de Unit of Work.

Além de persistir as alterações, a Unit of Work é responsável por publicar os Domain Events antes da conclusão da transação.

## Consequências

### Positivas

- Consistência transacional
- Ponto único para persistência
- Integração com Domain Events

### Negativas

- Maior responsabilidade concentrada em um componente

## Alternativas consideradas

Salvar alterações diretamente em cada repositório, o que dificultaria o controle transacional e a publicação consistente de eventos.