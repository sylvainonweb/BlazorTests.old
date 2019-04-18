using BlazorTests.Common.Data.Entities;
using BlazorTests.Common.Technical.Database;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace BlazorTests.Server.Entities.DatabaseSpecific
{
    public partial class DataAccessAdapter : IDataAccessAdapterEx
    {
        public string User { get; set; }
        public bool MustLogQueriesForSynchronization { get; set; }

        #region Définition des champs d'audit (utilisateur, date)

        protected override void OnBeforeEntitySave(IEntity2 entitySaved, bool insertAction)
        {
            //FIXME : A décommenté si besoin
            //EntityAuditFieldSetter.DefineAuditFields(entitySaved, insertAction, this.User);
            base.OnBeforeEntitySave(entitySaved, insertAction);
        }

        #endregion
    }
}
