# Testes

## Objetivo

Garantir que as regras de negócio permaneçam corretas ao longo da evolução do sistema.

---

# Ferramentas

- xUnit
- Moq
- FluentAssertions

---

# Executar todos os testes

```bash
dotnet test
```

---

# Executar um projeto específico

```bash
dotnet test tests/YFCore.Tests.Unit
```

---

# Cobertura

Exemplo utilizando Coverlet:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Boas práticas

- Um cenário por teste
- Nome descritivo
- Arrange
- Act
- Assert

Exemplo:

```
Should_Create_Appointment_When_Data_Is_Valid
```

---

# O que testar

- Entidades
- Value Objects
- Commands
- Queries
- Validators
- Domain Services
- Domain Events