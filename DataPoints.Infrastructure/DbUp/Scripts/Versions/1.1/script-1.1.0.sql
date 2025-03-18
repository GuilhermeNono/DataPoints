CREATE TABLE Blc_Block
(
    Id uniqueidentifier not null
        constraint PK_Block primary key
        constraint DF_Block default NEWID(),
    Hash varchar(512) not null,
    PreviousHash varchar(512) not null,
    DateInclusion DATETIME NOT NULL
)