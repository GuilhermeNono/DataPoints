-- TX-1 / SEC-2: Idempotency-Key / nonce anti-replay.
-- Port para PostgreSQL (ARC-4): uniqueidentifier -> uuid; datetime -> timestamptz; varchar(max) -> text.
CREATE TABLE Idm_IdempotencyKeys
(
    Id           uniqueidentifier not null
        constraint PK_IdempotencyKey primary key,
    RequestHash  varchar(128)     not null,
    Status       int              not null,
    ResponseBody varchar(max)     null,
    CreatedAt    Datetime         not null,
    ExpiresAt    Datetime         not null
)

go
