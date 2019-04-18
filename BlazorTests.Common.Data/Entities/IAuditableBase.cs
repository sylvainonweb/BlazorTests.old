using System;

namespace BlazorTests.Common.Data.Entities
{
    /// <summary>
    /// Interface à dériver dans chaque projet LLBLGEN afin que l'interface se trouve dans le même namespace que l'entité
    /// et que les entités générées compilent. On aurait pu modifier le template de génération des entités mais c'est trop de boulot pour pas grand chose
    /// </summary>
    public interface IAuditableBase
    {
        string CreePar { get; set; }
        DateTime CreeLe { get; set; }
        string ModifiePar { get; set; }
        DateTime ModifieLe { get; set; }
    }
}
