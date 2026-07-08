# Tratamento de Erros

A API utiliza códigos HTTP padronizados para indicar o resultado das operações.

| Código | Descrição |
|---------|-----------|
| 200 | Sucesso |
| 201 | Recurso criado |
| 204 | Operação concluída sem conteúdo |
| 400 | Requisição inválida |
| 401 | Não autenticado |
| 403 | Acesso negado |
| 404 | Recurso não encontrado |
| 409 | Conflito de negócio |
| 422 | Erro de validação |
| 500 | Erro interno |

## Exemplo de resposta de erro

```json
{
    "type": "https://httpstatuses.com/400",
    "title": "Validation Error",
    "status": 400,
    "errors": {
        "email": [
            "Email is required."
        ],
        "password": [
            "Password must contain at least 8 characters."
        ]
    }
}
```

## Validação

Erros de validação retornam todos os problemas encontrados na requisição, permitindo que o cliente corrija múltiplos campos em uma única tentativa.

## Regras de negócio

Quando uma regra de negócio impede a operação (por exemplo, agendar um horário já ocupado), a API retorna um código apropriado (`409 Conflict` ou outro definido pelo projeto) acompanhado de uma mensagem clara para o consumidor.