-- ARC-4: seed inicial do schema core (equivalente aos scripts 1.0.1/1.0.2/1.1.1/1.2.1 do SQL Server).
INSERT INTO core.dcm_types (id, name) VALUES (1, 'Cpf'), (2, 'Cnpj');

INSERT INTO core.prm_permissions (id, name, isblocked, operation, lastchangeat, lastchangeby)
VALUES (1, 'USER', false, 'C', now(), 'System#0'),
       (2, 'ADMINISTRATOR', false, 'C', now(), 'System#0');

INSERT INTO core.ppl_type (id, name) VALUES (1, 'NaturalPerson'), (2, 'LegalPerson');

INSERT INTO core.blc_block (hash, previoushash, merkleroot, isvalid, blocksignature, publickey, dateinclusion)
VALUES ('GenesisBlock', 'GENESIS', 'GenesisRoot', true, 'GenesisSignature',
        'MIGEAgEAMBAGByqGSM49AgEGBSuBBAAKBG0wawIBAQQgZlD6NU2dofEG7w1pvtXRE5h6gNYurJwXkIEEiLk4RBKhRANCAAQToZzp3OnDqrGlkYzFxoLyjYy608kQRWbR76Hg7manuLP52nJAAQLJQIV4Htl4hkKRqcqPw5I94obJgPcd6fyp',
        now());

INSERT INTO core.btc_status (id, name, description)
VALUES (1, 'Pending', 'Batch validation not started yet.'),
       (2, 'Processing', 'Batch validation is processing...'),
       (3, 'Completed', 'Batch validation it was completed.');

INSERT INTO core.btc_validations (beginvalidation, endvalidation, idbatchstatus, blockinvalidated, blockprocessed)
VALUES (now(), now(), 3, 0, 1);

INSERT INTO core.btc_checkpoints (idblock, idbatch, isvalid)
VALUES ((SELECT id FROM core.blc_block WHERE hash = 'GenesisBlock'),
        (SELECT id FROM core.btc_validations LIMIT 1),
        true);
