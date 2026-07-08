# API Documentation

## Introdução

A API do YFCore segue os princípios REST e utiliza JSON como formato padrão para troca de informações.

Todos os endpoints retornam códigos HTTP apropriados e, quando necessário, exigem autenticação via JWT Bearer Token.

---

# Base URL

```
https://localhost:5001/api
```

---

# Formato das requisições

Content-Type

```
application/json
```

---

# Autenticação

Endpoints protegidos exigem:

```
Authorization: Bearer {token}
```

---

# Versionamento

Atualmente a API utiliza a versão v1.

```
/api/v1
```

---

# Recursos

- Authentication
- Users
- Categories
- Procedure Types
- Appointments

---

# Respostas

A API utiliza JSON em todas as respostas.

Exemplo:

```json
{
    "id":"...",
    "name":"..."
}
```

---

# Erros

Consulte:

errors.md