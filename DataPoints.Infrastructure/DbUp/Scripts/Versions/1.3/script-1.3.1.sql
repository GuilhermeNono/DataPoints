-- SEC-4: colunas de lockout do ASP.NET Identity (IUserLockoutStore).
-- Port para PostgreSQL (ARC-4): datetimeoffset -> timestamptz; bit -> boolean.
ALTER TABLE Ath_Users ADD LockoutEnd datetimeoffset null

go

ALTER TABLE Ath_Users ADD AccessFailedCount int not null default 0

go

ALTER TABLE Ath_Users ADD LockoutEnabled bit not null default 1

go
