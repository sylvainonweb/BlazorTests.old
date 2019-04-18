using System.Collections.Generic;
using BlazorTests.Common.Data.Exceptions;

namespace BlazorTests.Common.Technical.Database.QueryObjects
{
    public abstract class QueryDatabaseObjectRetrieverBase
    {
        public string Execute(string sqlQuery)
        {
            IList<string> databaseObjects = new List<string>();

            string objectStartIndicator = GetObjectStartIndicator();
            string objectEndIndicator = GetObjectEndIndicator();

            int foundAt = sqlQuery.IndexOf(objectStartIndicator);
            while (foundAt > 0)
            {
                int startIndex = foundAt + objectStartIndicator.Length;

                foundAt = sqlQuery.IndexOf(objectEndIndicator, startIndex);
                int endIndex = foundAt + objectEndIndicator.Length;

                string databaseObject = sqlQuery.Substring(startIndex, endIndex - (startIndex + 1));
                if (!databaseObjects.Contains(databaseObject))
                {
                    databaseObjects.Add(databaseObject);
                }

                foundAt = sqlQuery.IndexOf(objectStartIndicator, endIndex);
            }

            if (databaseObjects.Count > 0)
            {
                return string.Join(", ", databaseObjects);
            }
            else
            {
                throw new DlmDatabaseException("Impossible de récupérer les objets utilisés par la requête");
            }
        }

        protected abstract string GetObjectStartIndicator();
        protected abstract string GetObjectEndIndicator();
    }
}