# Padrões de Código

## Princípios

- SOLID
- DRY
- KISS
- Clean Code

---

# Convenções

## Classes

PascalCase

```
CreateAppointmentCommand
```

---

## Métodos

PascalCase

```
Create()
```

---

## Variáveis

camelCase

```
appointment
```

---

## Interfaces

Prefixo I

```
IAppointmentRepository
```

---

## Namespaces

Devem refletir a estrutura de pastas.

---

# Organização

Cada caso de uso deve conter:

- Command ou Query
- Handler
- Validator
- DTOs (quando necessário)

---

# Controllers

Controllers não devem conter regra de negócio.

Seu papel é apenas receber a requisição e delegar para a camada Application.

---

# Domain

Toda regra de negócio deve permanecer na camada Domain sempre que possível.