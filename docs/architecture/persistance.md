# Persistência

O projeto utiliza Entity Framework Core.

O acesso ao banco ocorre através do Repository Pattern.

Fluxo:

Controller

↓

Handler

↓

Repository

↓

DbContext

↓

SQL Server

As interfaces de repositório ficam no Domain.

As implementações ficam na Infrastructure.

Isso permite substituir o mecanismo de persistência sem alterar regras de negócio.
