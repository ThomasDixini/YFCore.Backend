# ADR-0001 — Adoção da Clean Architecture

## Status

Aceito

## Contexto

O projeto precisava de uma arquitetura que permitisse crescimento contínuo sem acoplamento entre regras de negócio e detalhes técnicos.

## Decisão

Adotar a Clean Architecture separando o sistema nas seguintes camadas:

- API
- Application
- Domain
- Infrastructure

As dependências sempre apontam para o domínio.

## Consequências

### Positivas

- Alta testabilidade
- Independência de frameworks
- Facilidade de manutenção
- Baixo acoplamento

### Negativas

- Maior número de projetos
- Curva de aprendizado maior
- Mais abstrações

## Alternativas consideradas

### Arquitetura em Camadas

Descartada devido ao maior acoplamento entre regras de negócio e infraestrutura.

### MVC Tradicional

Adequado para aplicações pequenas, porém insuficiente para sistemas com regras de negócio complexas.