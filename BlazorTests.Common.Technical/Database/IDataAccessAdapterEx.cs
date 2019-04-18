using SD.LLBLGen.Pro.ORMSupportClasses;

namespace BlazorTests.Common.Technical.Database
{
    public interface IDataAccessAdapterEx : IDataAccessAdapter
    {
        string User { get; set; }
        bool MustLogQueriesForSynchronization { get; set; }
    }

    public enum DatabaseConnectionTypeEnumeration
    {
        NotDefined = 0,
        Local = 1,
        Remote = 2
    }

    public enum DatabaseConnectionModeEnumeration
    {
        NotDefined = 0,
        NomadicConnected = 1,    //PC nomade connecté au serveur
        NomadicDisconnected = 2, //PC nomade non connecté au serveur
        DirectConnection = 3, //Poste fixe connecté au serveur
    }
}
