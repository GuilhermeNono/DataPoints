alter table Wlt_Transactions
    Add IsCredit bit not null constraint DF_transactions default 0

go

alter table Wlt_Transactions
    drop constraint DF_transactions