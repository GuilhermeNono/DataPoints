CREATE TABLE Btc_Status
(
    Id          int          not null
        constraint PK_BatchStatus primary key,
    Name        varchar(120) not null,
    Description varchar(255) not null
)
    go

CREATE TABLE Btc_Validations
(
    Id              uniqueidentifier not null
        constraint PK_BatchValidation primary key constraint Df_BatchValidation default newid(),
    BeginValidation DATETIME         not null,
    EndValidation   DATETIME             null,
    IdBatchStatus   int              not null
        constraint FK_BatchValidation_BatchStatus references Btc_Status,
    BlockInvalidated int not null,
    BlockProcessed int not null
)

go

CREATE TABLE Btc_Checkpoints(
    Id bigint not null constraint PK_BatchCheckpoint primary key identity,
    IdBlock uniqueidentifier not null constraint FK_BatchCheckpoint_Block references Blc_Block,
    IdBatch uniqueidentifier not null constraint FK_BatchCheckpoint_BatchValidation references Btc_Validations,
    IsValid bit not null
)