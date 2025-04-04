CREATE TABLE Blc_Block
(
    Id             uniqueidentifier not null
        constraint PK_Block primary key
        constraint DF_Block default NEWID(),
    Hash           varchar(512)     not null,
    MerkleRoot     varchar(512)     not null,
    BlockSignature varchar(255)         null,
    PublicKey      varchar(800)     not null,
    IsValid        bit              not null,
    PreviousHash   varchar(512)     not null,
    DateInclusion  DATETIME         NOT NULL
)