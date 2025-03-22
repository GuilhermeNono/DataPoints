insert into Dcm_Types(Id, Name)
values(1, 'Cpf'),
      (2, 'Cnpj')

go

insert into Prm_Permissions(Id, Name, IsBlocked, Operation, LastChangeAt, LastChangeBy)
values (1, 'USER', 0, 'C', getdate(), 'System#0'),
       (2, 'ADMINISTRATOR', 0, 'C', getdate(), 'System#0')
