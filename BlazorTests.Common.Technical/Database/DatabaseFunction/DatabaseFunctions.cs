using SD.LLBLGen.Pro.QuerySpec;

namespace BlazorTests.Common.Technical.Database.DatabaseFunction
{
    public static class DatabaseFunctions
    {
        public static FunctionMappingExpression Concat(object arg1, object arg2)
        {
            return new FunctionMappingExpression(typeof(DatabaseFunctions), "CONCAT", 2, arg1, arg2);
        }
    }
}
