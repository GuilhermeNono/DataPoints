namespace DataPoints.Crosscutting.Messages;

public static class ErrorMessage
{
    /// <summary>
    /// Classe Responsável pelas Mensagens de Exceção do Sistema
    /// </summary>
    public static class Exception
    {
        public static string ExternalOrderWithInternalPagination() =>
            "Não é possivel utilizar um Order By interno(dentro do método prepare) e um pagination ou Order externo. Considere Utilizar apenas de uma maneira.";

        public static string EntityNotFound(long id, string entityName) =>
            $"Não foi possível encontrar um registro de Id: {id} para {entityName}";
        
        public static string EntityNotFound(Guid id, string entityName) =>
            $"Não foi possível encontrar um registro de Id: {id} para {entityName}";

        public static string EntityNotFound(string entityName) =>
            $"Não foi possível encontrar um registro para {entityName}";
        
        public static string EntityDeleteNotFoundException(string entity) =>
            $"Não foi possível deletar, pois não há registro em {entity} com este Id";
        
        public static string MigrationFailed() => "Houve uma falha ao executar a migração do banco de dados.";
    }
}
