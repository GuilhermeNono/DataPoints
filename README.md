# 💠 DataPoints

**DataPoints** é uma API para transferência de pontos internos entre usuários, sustentada por um ledger no estilo blockchain privado. Cada transferência é agrupada em um bloco encadeado ao anterior (hash + Merkle root sobre as transações), garantindo evidência de violação (tamper-evidence) sobre todo o histórico.

Desenvolvido em **C#** com **.NET 10**, **DbUp** e **Entity Framework Core** sobre **PostgreSQL (Supabase)**.

## 📖 Sobre o projeto

O objetivo é oferecer um sistema seguro e transparente para distribuir e transacionar pontos entre usuários, sem depender de confiança cega em um banco de dados mutável. Toda transferência exige uma assinatura ECDSA gerada no cliente (a chave privada nunca sai do dispositivo do usuário), e um job em background valida continuamente a integridade da cadeia.

## 🚀 Tecnologias

| Camada | Tecnologia |
| --- | --- |
| Linguagem / Runtime | **C# / .NET 10** |
| Banco de dados | **PostgreSQL** (Supabase) via **Npgsql** / **Entity Framework Core** |
| Migrations | **DbUp** (versionadas em SQL, aplicadas automaticamente no boot) |
| Assinatura criptográfica | **NBitcoin** (ECDSA) |
| Jobs em background | **Hangfire** (validação contínua da cadeia) |
| Mensageria de comandos/queries | **MediatR** |

## ✨ Funcionalidades

- 🔐 **Autenticação & SSO** — sign-in, sign-up, refresh token e integração de single sign-on
- 💸 **Transferência de pontos** entre usuários, autorizada por assinatura ECDSA client-side
- 🧾 **Idempotência** de transferências via header `Idempotency-Key`, com payloads assinados anti-replay
- 💰 **Consulta de saldo** (`wallets/balance`) e histórico de transações por bloco
- ⛓️ **Cadeia de blocos** com hash encadeado e Merkle root, validada continuamente em background
- 🛡️ **Row-Level Security (RLS)** em todas as tabelas, somada à autorização em nível de aplicação
- 🚦 **Rate limiting** em rotas sensíveis (autenticação e SSO)

## 🏗️ Arquitetura

O projeto segue uma organização em camadas (Clean Architecture):

- `DataPoints.Api` — ponto de entrada, configuração e bootstrap
- `DataPoints.Presentation` — controllers e contratos de rota HTTP
- `DataPoints.Application` — casos de uso (commands/queries via MediatR)
- `DataPoints.Domain` — entidades, regras de negócio e interfaces
- `DataPoints.Contract` — DTOs de request/response
- `DataPoints.Infrastructure` — persistência, EF Core e scripts DbUp
- `DataPoints.Crosscutting` / `DataPoints.Crosscutting.Mapper` — utilitários e mapeamentos compartilhados
- `DataPoints.Domain.Tests` — testes unitários do domínio

## ⚙️ Como executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/GuilhermeNono/DataPoints.git
   ```
2. Acesse a pasta do projeto:
   ```bash
   cd DataPoints
   ```
3. Restaure as dependências:
   ```bash
   dotnet restore
   ```
4. Configure a connection string do PostgreSQL/Supabase via User Secrets (nunca commite em `appsettings*.json`):
   ```bash
   cd DataPoints.Api
   dotnet user-secrets set "ConnectionStrings:MainDatabase" "Host=...;Port=6543;Database=postgres;Username=app_user;Password=...;SSL Mode=Require"
   dotnet user-secrets set "JwtConfiguration:SecretKey" "<32+ bytes aleatórios, base64>"
   dotnet user-secrets set "ChainSigningConfiguration:PublicKey" "<chave pública ECDSA do sistema, base64>"
   dotnet user-secrets set "ChainSigningConfiguration:PrivateKey" "<chave privada ECDSA do sistema, base64>"
   ```
   Veja [DataPoints.Api/appsettings.Development.example.json](DataPoints.Api/appsettings.Development.example.json) para o formato esperado.
5. Suba a aplicação (as migrations, incluindo as políticas de RLS, rodam automaticamente no boot):
   ```bash
   dotnet run --project DataPoints.Api
   ```

> ℹ️ Não há passo manual de migration (`dotnet ef ...`) — as mudanças de schema vivem em scripts SQL versionados em `DataPoints.Infrastructure/DbUp/Scripts/Postgres/` e são aplicadas pelo DbUp na subida da API.

## 🤝 Contribuindo

Quer contribuir com o projeto? Siga os passos:

1. Faça um fork do repositório
2. Crie uma branch para a sua feature (`git checkout -b feature/nova-feature`)
3. Faça commit das suas alterações (`git commit -m 'Add nova feature'`)
4. Envie para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

Desenvolvido com 💙 por [GuilhermeNono](https://github.com/GuilhermeNono)