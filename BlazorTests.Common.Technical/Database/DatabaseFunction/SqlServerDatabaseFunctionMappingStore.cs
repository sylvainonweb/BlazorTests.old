using SD.LLBLGen.Pro.ORMSupportClasses;

namespace BlazorTests.Common.Technical.Database.DatabaseFunction
{
    public class SqlServerDatabaseFunctionMappingStore : FunctionMappingStore
    {
        public SqlServerDatabaseFunctionMappingStore()
            : base()
        {
            this.Add(new FunctionMapping(typeof(DatabaseFunctions), "CONCAT", 2,
                "CASE " +
                "   WHEN {0} IS NULL AND {1} IS NULL " +
                "       THEN '' " +
                "   WHEN {0} IS NOT NULL AND {1} IS NULL " +
                "       THEN {0} " +
                "   WHEN {0} IS NULL AND {1} IS NOT NULL " +
                "       THEN {1} " +
                "   ELSE " +
                "       {0} + ' ' + {1} " +
                "END "));
        }
    }
}
