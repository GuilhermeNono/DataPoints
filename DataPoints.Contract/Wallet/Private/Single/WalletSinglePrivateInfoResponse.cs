namespace DataPoints.Contract.Wallet.Private.Single;

public class WalletSinglePrivateInfoResponse
{
    public string Warning => "Aviso! A chave abaixo é gerada apenas neste momento e não poderá ser recuperada posteriormente. Certifique-se de armazená-la com segurança, pois ela será necessária para assinar suas transações. Sem essa chave, não será possível transferir seus pontos.";
    public string Key { get; set; } = string.Empty;
}