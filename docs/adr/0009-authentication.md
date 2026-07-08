# ADR-0009 — Autenticação com JWT

## Status

Aceito

## Contexto

A API será consumida por clientes externos e necessita de um mecanismo de autenticação stateless que permita escalabilidade horizontal sem dependência de sessões armazenadas no servidor.

## Decisão

Foi adotada autenticação baseada em JSON Web Token (JWT).

Após a autenticação do usuário, a API gera um Access Token contendo as informações necessárias para identificação e autorização do usuário.

Todas as requisições protegidas devem enviar o token utilizando o esquema:

Authorization: Bearer {token}

## Consequências

### Positivas

- API stateless.
- Fácil integração com aplicações Web e Mobile.
- Escalabilidade horizontal.
- Não requer armazenamento de sessão no servidor.
- Amplo suporte pela plataforma .NET.

### Negativas

- Revogação de tokens exige estratégia adicional.
- Tempo de expiração deve ser cuidadosamente definido.
- Necessidade de proteger adequadamente a chave de assinatura.

## Alternativas consideradas

### Cookie Authentication

Mais indicado para aplicações MVC tradicionais, porém menos adequado para APIs REST consumidas por múltiplos clientes.

### Session Authentication

Descartado por exigir armazenamento de estado no servidor, dificultando a escalabilidade.

### OAuth2 Completo

Embora mais robusto, foi considerado complexo para os requisitos atuais da aplicação.

## Justificativa

JWT fornece uma solução simples, amplamente adotada e alinhada às necessidades de uma API REST moderna.