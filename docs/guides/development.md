# Guia de Desenvolvimento

## Objetivo

Este documento descreve o ambiente necessário para desenvolver o YFCore Backend.

---

# Pré-requisitos

Antes de iniciar, instale:

- .NET SDK 10
- Docker Desktop
- SQL Server (ou SQL Server em Docker)
- Git
- Visual Studio 2022 ou Visual Studio Code

---

# Clonando o projeto

```bash
git clone https://github.com/<usuario>/YFCore.Backend.git

cd YFCore.Backend
```

---

# Restaurando dependências

```bash
dotnet restore
```

---

# Configurando a aplicação

Configure o arquivo:

```
appsettings.Development.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YFCore;Trusted_Connection=True;TrustServerCertificate=True"
  },

  "Jwt": {
    "Issuer": "YFCore",
    "Audience": "YFCore",
    "SecretKey": "YOUR_SECRET_KEY"
  }
}
```

---

# Criando o banco

```bash
dotnet ef database update
```

---

# Executando

```bash
dotnet run --project src/YFCore.Api
```

---

# Estrutura da solução

```
src/

tests/

docs/
```

---

# Fluxo recomendado

1. Criar uma Branch
2. Desenvolver
3. Executar testes
4. Criar Pull Request