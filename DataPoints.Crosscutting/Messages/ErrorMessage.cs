namespace DataPoints.Crosscutting.Messages;

public static class ErrorMessage
{
    /// <summary>
    /// Classe Responsável pelas Mensagens de Exceção do Sistema
    /// </summary>
    public static class Exception
    {
        public static string ExternalOrderWithInternalPagination() =>
            "Não é possível utilizar um Order By interno(dentro do método prepare) e um pagination ou Order externo. Considere Utilizar apenas de uma maneira.";

        public static string EntityNotFound(long id, string entityName) =>
            $"Não foi possível encontrar um registro de Id: {id} para {entityName}.";
        
        public static string EntityNotFound(Guid id, string entityName) =>
            $"Não foi possível encontrar um registro de Id: {id} para {entityName}.";

        public static string EntityNotFound(string entityName) =>
            $"Não foi possível encontrar um registro para {entityName}.";
        
        public static string EntityDeleteNotFoundException(string entity) =>
            $"Não foi possível deletar, pois não há registro em {entity} com este Id.";
        
        public static string MigrationFailed() => "Houve uma falha ao executar a migração do banco de dados.";

        public static string UserNotFound(Guid id) => $"Não foi possível encontrar um usuário com o Id {id}";

        public static string LoginNotFound() => "Credenciais inexistentes ou inválidas.";

        public static string DocumentEmpty() => "O numero do documento não pode ser nulo e nem vazio.";

        public static string DocumentNumberHasToBeFormated(string documentNumber) => $"O numero do document {documentNumber} é inválido. Envie o numero utilizando a mascara 000.000.000-00(CPF) ou 00.000.000/0000-00(CNPJ).";

        public static string DocumentCpfInvalid() => "O numero de CPF não é válido.";

        public static string DocumentCnpjInvalid() => "O numero d CNPJ não é válido.";

        public static string UserEmailNotFound() => "Não foi possível encontrar o email desse usuario.";

        public static string EmailIsAlreadyInUseException(string email) => $"O e-mail: {email} já está sendo utilizado.";

        public static string DocumentIsAlreadyInUseException(string document) =>
            $"O documento: {document} já está sendo utilizado.";

        public static string UserAlreadyHasThatRole(string role) => $"Esse usuario já possui o cargo {role}.";

        public static string PermissionRoleNotFound(string role) =>
            $"Não foi possível encontrar um cargo de nome {role}.";

        public static string PersonNotFound() => "Não foi possível encontrar um usuário com esse Id.";

        public static string RefreshNotFound() => "Não foi possível encontrar esse refresh token.";

        public static string ExpiredTokenException() => "O token se encontra expirado.";

        public static string WalletPublicKeyNotFound(string publicKey) => $"Não foi encontrada nem uma carteira com a chave {publicKey}.";

        public static string WalletUserNotFound(Guid userId) => $"Nâo foi possivel encontrar uma carteira para o usuario {userId}.";

        public static string WalletNotFound(Guid walletId) => $"A carteira de id {walletId} não foi encontrada.";

        public static string LastBlockNotFound() =>
            "Não foi possivel completar a transação, pois o ultimo bloco não foi encontrado.";

        public static string TransactionsNotFound() => "Não foi possivel localizar as transações.";

        public static string LoggedPersonNotFound() => "Não foi possivel encontrar o usuario logado na base de dados.";
    }
}
