# Authentication

Responsável pela autenticação de usuários.

---

## Login

POST

```
/api/auth/login
```

### Request

```json
{
    "email":"john@email.com",
    "password":"123456"
}
```

### Response

200 OK

```json
{
    "accessToken":"...",
    "expiresIn":3600
}
```

---

## Register

POST

```
/api/auth/register
```

### Request

```json
{
    "name":"John",
    "email":"john@email.com",
    "password":"123456"
}
```

### Response

201 Created