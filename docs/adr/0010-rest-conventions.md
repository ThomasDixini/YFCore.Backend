# ADR-0010 — Convenções REST

## Status

Aceito

## Contexto

A API necessita seguir um padrão consistente para facilitar seu consumo, manutenção e evolução.

Uma padronização reduz ambiguidades e melhora a experiência dos consumidores da API.

## Decisão

A API seguirá os princípios REST utilizando recursos como representação principal da aplicação.

As convenções adotadas incluem:

- Recursos nomeados no plural.
- Utilização correta dos verbos HTTP.
- Uso apropriado dos códigos de status.
- Endpoints orientados a recursos.
- Versionamento preparado para evolução futura.
- DTOs para entrada e saída de dados.
- Padronização das respostas de erro.

### Métodos HTTP

| Método | Finalidade |
|---------|------------|
| GET | Consultar recursos |
| POST | Criar recursos |
| PUT | Atualizar completamente |
| PATCH | Atualizar parcialmente |
| DELETE | Remover recursos |

### Códigos HTTP

| Código | Significado |
|---------|-------------|
| 200 | Operação realizada com sucesso |
| 201 | Recurso criado |
| 204 | Operação sem conteúdo de retorno |
| 400 | Requisição inválida |
| 401 | Não autenticado |
| 403 | Acesso negado |
| 404 | Recurso não encontrado |
| 409 | Conflito de negócio |
| 422 | Erro de validação |
| 500 | Erro interno |

## Consequências

### Positivas

- API previsível.
- Facilidade de integração.
- Melhor documentação.
- Menor curva de aprendizado para consumidores.

### Negativas

- Algumas operações específicas do domínio podem exigir endpoints orientados por ação (por exemplo, confirmação ou cancelamento de um agendamento), demandando atenção para manter a consistência com os princípios REST.

## Alternativas consideradas

### RPC (Remote Procedure Call)

Endpoints como:

- `/CreateAppointment`
- `/DeleteAppointment`
- `/ConfirmAppointment`

Foram descartados por exporem ações em vez de recursos e reduzirem a aderência ao modelo REST.

## Justificativa

As convenções REST promovem interoperabilidade, padronização e facilidade de evolução da API, além de estarem alinhadas às recomendações amplamente adotadas pela comunidade.