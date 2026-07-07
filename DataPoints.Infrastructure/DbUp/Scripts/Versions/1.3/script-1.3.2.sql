-- SEC-5: rotação e revogação de refresh token. A coluna Token passa a guardar hash SHA-256 (não mais o valor em claro).
-- Port para PostgreSQL (ARC-4): bigint -> bigint (igual); datetime -> timestamptz.
ALTER TABLE Ath_RefreshTokens ADD RevokedAt datetime null

go

ALTER TABLE Ath_RefreshTokens ADD ReplacedByTokenId bigint null

go
