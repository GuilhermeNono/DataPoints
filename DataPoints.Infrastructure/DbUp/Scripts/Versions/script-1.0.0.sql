Create table Ath_Users
(
    Id               uniqueidentifier not null
        constraint PK_User primary key
        constraint DF_User_Id default newsequentialid(),
    Email            varchar(120)     not null,
    NormalizedEmail  varchar(120)     not null,
    SecurityStamp    varchar(120)     not null,
    IsEmailConfirmed bit              not null,
    PasswordHash     varchar(258)     not null,
    IsActive         bit              not null,
    Operation        char(1)          not null,
    LastChangeAt     Datetime         not null,
    LastChangeBy     Varchar(255)     not null
)

go

CREATE TABLE Dcm_Types
(
    Id   int          not null
        constraint PK_Document primary key,
    Name varchar(125) not null
)

go

CREATE TABLE Ppl_Type
(
    Id   int          not null
        constraint PK_PersonType primary key,
    Name varchar(125) not null
)

go

CREATE TABLE Ppl_People
(
    Id                       uniqueidentifier not null
        constraint PK_Person primary key
        references Ath_Users (Id),
    FirstName                varchar(125)     not null,
    LastName                 varchar(125)     not null,
    Avatar                   varchar(800)     null,
    BirthDate                DateTime         not null,
    IsActive                 Bit              not null,
    DocumentNumber           varchar(80)      not null,
    NormalizedDocumentNumber varchar(80)      not null,
    DocumentType             int              not null
        constraint FK_Person_Document references Dcm_Types (Id),
    PersonType               int              not null
        constraint FK_Person_Type references Ppl_Type (Id),
    DateInclusion            Datetime         not null,
    Operation                char(1)          not null,
    LastChangeAt             Datetime         not null,
    LastChangeBy             Varchar(255)     not null
)

go

CREATE TABLE Wlt_Wallets
(
    Id           uniqueidentifier not null
        constraint PK_Wallet primary key
        constraint DF_Wallet_Id default NEWID(),
    IdUser       uniqueidentifier not null
        constraint FK_Wallet_User references Ath_Users,
    PublicKey    varchar(800)     not null,
    Balance      Decimal(18, 8)   not null,
    IsBlocked    Bit              not null,
    IsActive     Bit              not null,
    Operation    char(1)          not null,
    LastChangeAt Datetime         not null,
    LastChangeBy Varchar(255)     not null
)

go

CREATE TABLE Wlt_Transactions
(
    Id              uniqueidentifier not null
        constraint PK_WalletTransaction primary key
        constraint DF_WalletTransaction_Id default NEWID(),
    IdWalletFrom    uniqueidentifier not null
        constraint FK_WalletTransaction_WalletFrom references Wlt_Wallets,
    IdWalletTo      uniqueidentifier not null
        constraint FK_WalletTransaction_WalletTo references Wlt_Wallets,
    Amount          Decimal(18, 8)   not null,
    TransactionHash Varchar(800)     not null,
    Operation       char(1)          not null,
    LastChangeAt    Datetime         not null,
    LastChangeBy    Varchar(255)     not null
)

go

create index IDX_WalletFrom_WalletTo On Wlt_Transactions (IdWalletFrom, IdWalletTo)

go

CREATE TABLE Prm_Permissions
(
    Id           int          not null
        constraint PK_Permissions primary key,
    Name         varchar(150) not null,
    IsBlocked    Bit          not null,
    Operation    char(1)      not null,
    LastChangeAt Datetime     not null,
    LastChangeBy Varchar(255) not null
)

go

CREATE TABLE Prm_Profiles
(
    Id           bigint           not null
        constraint PK_Profile primary key
        identity,
    IdUser       uniqueidentifier not null
        constraint FK_Profile_User references Ath_Users,
    IdPermission int              not null
        constraint FK_Profile_Permission references Prm_Permissions,
    Operation    char(1)          not null,
    LastChangeAt Datetime         not null,
    LastChangeBy Varchar(255)     not null
)

go

CREATE TABLE Ath_RefreshTokens
(
    Id           bigint           not null
        constraint PK_RefreshToken primary key
        identity,
    Token        varchar(80)      not null,
    IdUser       uniqueidentifier not null
        constraint FK_RefreshToken_User references Ath_Users,
    DateExpired  DATETIME         NOT NULL,
    Operation    char(1)          not null,
    LastChangeAt Datetime         not null,
    LastChangeBy Varchar(255)     not null
)
