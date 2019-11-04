namespace mcs.components.DbConnection
{
    public static class SqlQueryGenerator
    {

        private static string DeleteQuery = "";

        public static string GenerateSelectQuery<T>(SqlCommandHelper<T> sqlcommand)
        {
            var insertQuery = "Insert into @table (@columns) Values(@value);";
            return insertQuery;
        }

        public static string GenerateUpdateQuery<T>(SqlCommandHelper<T> sqlcommand)
        {
            var updateQuery = "Update @table set @column = @value Where @find";
            return updateQuery;
        }

        public static string GenerateDeleteQuery<T>(SqlCommandHelper<T> sqlcommand)
        {
            var deleteQuery = "";
            return deleteQuery;
        }

    }
}