insert into Btc_Status(Id, Name, Description)
values (1, 'Pending', 'Batch validation not started yet.'),
       (2, 'Processing', 'Batch validation is processing...'),
       (3, 'Completed', 'Batch validation it was completed.')

go

insert into Btc_Validations(BeginValidation, EndValidation, IdBatchStatus, BlockInvalidated, BlockProcessed)
values (getdate(), getdate(), 3, 0, 1)

go

insert into Btc_Checkpoints(IdBlock, IdBatch, IsValid)
values((select Id from Blc_Block where Hash = 'GenesisBlock'), (select Top(1)Id from Btc_Validations), 1)