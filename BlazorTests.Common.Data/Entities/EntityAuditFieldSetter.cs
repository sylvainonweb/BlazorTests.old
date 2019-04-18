using SD.LLBLGen.Pro.ORMSupportClasses;
using System;

namespace BlazorTests.Common.Data.Entities
{
    /// <summary>
    /// Méthode à appeler dans DataAccessAdapter.OnBeforeSave afin de définir les champs d'audit (CrééPar, ...)
    /// </summary>
    public class EntityAuditFieldSetter
    {
        public static void DefineAuditFields(IEntity2 entity, bool insertAction, string user)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                throw new ApplicationException("Impossible d'effectuer une sauvegarde : l'utilisateur n'est pas défini => les champs d'audit ne pourraient pas être renseignés");
            }

            IAuditableBase auditable = entity as IAuditableBase;
            if (auditable != null)
            {
                if (insertAction)
                {
                    auditable.CreeLe = DateTime.Now;
                    auditable.CreePar = user;
                }

                auditable.ModifieLe = DateTime.Now;
                auditable.ModifiePar = user;
            }
        }
    }
}
