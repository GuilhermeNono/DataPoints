insert into Blc_Block(Hash, PreviousHash, MerkleRoot, IsValid, BlockSignature, PublicKey, DateInclusion)
values ('GenesisBlock', 'GENESIS', 'GenesisRoot', 1, 'GenesisSignature',
        'MIGEAgEAMBAGByqGSM49AgEGBSuBBAAKBG0wawIBAQQgZlD6NU2dofEG7w1pvtXRE5h6gNYurJwXkIEEiLk4RBKhRANCAAQToZzp3OnDqrGlkYzFxoLyjYy608kQRWbR76Hg7manuLP52nJAAQLJQIV4Htl4hkKRqcqPw5I94obJgPcd6fyp',
        getdate())