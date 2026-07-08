# ADR-0008 — Entity Framework Core

## Status

Aceito

## Contexto

O sistema necessita de uma solução de persistência robusta, madura e bem integrada ao ecossistema .NET, capaz de suportar consultas complexas, migrações, rastreamento de entidades e mapeamento objeto-relacional.

Além disso, a solução deveria permitir que o domínio permanecesse independente da tecnologia de persistência utilizada.

## Decisão

Foi adotado o Entity Framework Core como ORM da aplicação.

A camada Domain define apenas os contratos (interfaces de repositório), enquanto a camada Infrastructure implementa esses contratos utilizando o Entity Framework Core.

As configurações das entidades são realizadas através da Fluent API utilizando classes que implementam `IEntityTypeConfiguration<T>`.

## Consequências

### Positivas

- Excelente integração com ASP.NET Core.
- Suporte nativo a migrations.
- Controle de transações.
- Change Tracking.
- Consultas utilizando LINQ.
- Facilidade para testes utilizando bancos em memória.
- Grande comunidade e documentação.

### Negativas

- Sobrecarga do Change Tracker em cenários específicos.
- Necessidade de conhecer boas práticas para evitar consultas ineficientes.
- Possibilidade de geração de SQL inadequado caso consultas não sejam bem projetadas.

## Alternativas consideradas

### Dapper

Apresenta maior desempenho em consultas simples, porém exigiria maior esforço na implementação da camada de persistência e no gerenciamento de relacionamentos.

### ADO.NET

Oferece controle total sobre o SQL, porém aumenta significativamente a quantidade de código de infraestrutura e reduz a produtividade.

## Justificativa

O Entity Framework Core oferece o melhor equilíbrio entre produtividade, manutenção e desempenho para o contexto deste projeto.