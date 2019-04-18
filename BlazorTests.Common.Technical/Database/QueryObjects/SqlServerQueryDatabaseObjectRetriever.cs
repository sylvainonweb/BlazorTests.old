
namespace BlazorTests.Common.Technical.Database.QueryObjects
{
    public class SqlServerQueryDatabaseObjectRetriever : QueryDatabaseObjectRetrieverBase
    {
        protected override string GetObjectStartIndicator()
        {
            // Peut poser problème si les tables sont crées avec un autre schéma. 
            // Peu de chance mais on ne sait jamais
            string SCHEMA_NAME = "dbo";
            return string.Format("[{0}].[", SCHEMA_NAME);
        }

        protected override string GetObjectEndIndicator()
        {
            return "]";
        }
    }
}