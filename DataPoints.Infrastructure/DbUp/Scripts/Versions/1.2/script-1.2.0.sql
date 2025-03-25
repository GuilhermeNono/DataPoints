CREATE TABLE Blc_BatchValidations(
    Id bigint not null constraint PK_BlockBatchValidation primary key identity,
    DateValidation DATETIME NOT NULL,
    Block
)