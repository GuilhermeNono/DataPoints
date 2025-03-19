alter table Wlt_Transactions
    add constraint FK_Transactions_Blocks foreign key(IdBlock) references Blc_Block(Id)