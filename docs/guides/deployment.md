# Deployment

## Objetivo

Publicar a API em ambiente de produção de forma segura e reproduzível.

---

# Build

```bash
dotnet publish -c Release
```

---

# Docker

```bash
docker build -t yfcore-api .
```

```bash
docker run -p 8080:80 yfcore-api
```

---

# Variáveis de ambiente

Configure:

- ConnectionStrings__DefaultConnection
- Jwt__Issuer
- Jwt__Audience
- Jwt__SecretKey

---

# Banco

Antes da publicação execute:

```bash
dotnet ef database update
```

---

# Checklist

- Build realizado
- Testes aprovados
- Migrações aplicadas
- Variáveis configuradas
- Logs monitorados