Create table Ath_Users
(
    Id               bigint           not null
        constraint PK_User primary key identity,
    IdUser           uniqueidentifier not null,
    Email            varchar(120)     not null,
    IsEmailConfirmed bit              not null,
    NormalizedEmail  varchar(120)     not null,
    SecurityStamp    varchar(120)     not null,
    PasswordHash     varchar(258)     not null,
    IsActive         bit              not null,
    Operation        char(1)          not null,
    LastChangeAt     Datetime         not null,
    LastChangeBy     Varchar(255)     not null
)
go

create index IDX_UserId on Ath_Users (IdUser)

go

CREATE TABLE Ppl_People
(
    Id                       bigint           not null
        constraint PK_Person primary key identity,
    IdPerson                 uniqueidentifier not null,
    FirstName                varchar(125)     not null,
    LastName                 varchar(125)     not null,
    Avatar                   varchar(800)     null,
    BirthDate                DateTime         not null,
    IsActive                 Bit              not null,
    DocumentNumber           varchar(80)      not null,
    NormalizedDocumentNumber varchar(80)      not null,
    DocumentType             int              not null,
    PersonType               int              not null,
    DateInclusion            Datetime         not null,
    Operation                char(1)          not null,
    LastChangeAt             Datetime         not null,
    LastChangeBy             Varchar(255)     not null
)
go

create index IDX_PeopleId on Ppl_People (IdPerson)

go

CREATE TABLE Wlt_Wallets
(
    Id           bigint           not null
        constraint PK_Wallet primary key identity,
    IdWallet     uniqueidentifier not null,
    IdUser       uniqueidentifier not null,
    PublicKey    varchar(800)     not null,
    Hash         varchar(255)     not null,
    IsBlocked    Bit              not null,
    IsActive     Bit              not null,
    Operation    char(1)          not null,
    LastChangeAt Datetime         not null,
    LastChangeBy Varchar(255)     not null
)

go

create index IDX_WalletId on Wlt_Wallets (IdWallet)

go

CREATE TABLE Prm_Permissions
(
    Id           bigint       not null
        constraint PK_Permission primary key identity,
    IdPermission int          not null,
    Name         varchar(150) not null,
    IsBlocked    Bit          not null,
    Operation    char(1)      not null,
    LastChangeAt Datetime     not null,
    LastChangeBy Varchar(255) not null
)

go

create index IDX_PermissionId on Prm_Permissions (IdPermission)

go

CREATE TABLE Prm_Profiles
(
    Id           bigint           not null
        constraint PK_Profile primary key identity,
    IdProfile    bigint           not null,
    IdUser       uniqueidentifier not null,
    IdPermission int              not null,
    Operation    char(1)          not null,
    LastChangeAt Datetime         not null,
    LastChangeBy Varchar(255)     not null
)

create index IDX_ProfileId on Prm_Profiles (IdProfile)
