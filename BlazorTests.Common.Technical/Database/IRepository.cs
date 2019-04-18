using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorTests.Common.Technical.Database
{
    /// <summary>
    /// Je sais, ça fait beaucoup de méthode mais je ne pouvais pas dériver de DataAccessAdapterBase vu 
    /// que le DataAccessAdapter est générée et donc différente à chaque projet
    /// </summary>
    public interface IRepository
    {
        #region Propriétés

        /// <summary>
        /// Chaine de connexion utilisée
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Indique s'il faut logger les requêtes exécutées sur la base dans une table de synchronisation
        /// </summary>
        bool MustLogQueriesForSynchronization { get; set; }

        /// <summary>
        /// Définit le timeout pour les commandes executées avec ce DataAccessAdapter
        /// </summary>
        int CommandTimeOut { get; set; }

        /// <summary>
        /// Utilisateur connecté. Permet de renseigner les champs CrééPar, ModifiéPar en automatique
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Liste des fichiers à supprimer au moment du commit.
        /// Utile afin de ne pas supprimer des fichiers alors que la transaction est rollbackée
        /// </summary>
        IList<string> FilesToDeleteOnCommit { get; }

        /// <summary>
        /// Utile car sinon la liste n'est jamais vidée et au prochain Save de l'entité principale,
        /// on réessayerait de supprimer l'entité présente dans la collection RemovedEntitiesTracker => OutOfSyncException
        /// </summary>
        IList<IEntityCollection2> RemovedEntitiesTrackerCollectionsToClearOnCommit { get; }

        #endregion

        T Get<T,U>(U id) where T : EntityBase2;
        T Get<T,U>(U id, PrefetchPath2 prefetchPath, bool throwExceptionIfNotExists) where T : EntityBase2;

        TEntity FetchSingle<TEntity>(EntityQuery<TEntity> query) where TEntity : EntityBase2, IEntity2;
        TElement FetchSingle<TElement>(DynamicQuery<TElement> query);

        TEntity FetchFirst<TEntity>(EntityQuery<TEntity> query) where TEntity : EntityBase2, IEntity2;
        TElement FetchFirst<TElement>(DynamicQuery<TElement> query);

        IEntityCollection2 FetchQuery<TEntity>(EntityQuery<TEntity> query) where TEntity : IEntity2;
        TCollection FetchQuery<TEntity, TCollection>(EntityQuery<TEntity> query, TCollection collectionToFill)
            where TEntity : IEntity2
            where TCollection : IEntityCollection2;
        IList<TElement> FetchQuery<TElement>(DynamicQuery<TElement> query);
        IList<object[]> FetchQuery(DynamicQuery query);
        
        TValue FetchScalar<TValue>(DynamicQuery query);
        void FetchEntityCollection(IEntityCollection2 collectionToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, ISortExpression sortClauses);

        void Save<T>(T entity, bool refetchAfterSave = true) where T : EntityBase2;
        void UpdateDirectly<T>(T entityContainingToBeChangedProperties, IRelationPredicateBucket bucket) where T : EntityBase2;

        void Delete<T>(T entity) where T : EntityBase2;
        void Delete(IEntityCollection2 collection);
        int DeleteEntitiesDirectly(Type typeOfEntity, IRelationPredicateBucket filterBucket);

        void ExecuteSQL(string rawSQL);

        Task ExecuteSQLAsync(string rawSQL);

        int Count(DynamicQuery query, DynamicQuery intermediateQuery);

        void StartTransaction();
        void Commit();
        void Rollback();
    }
}
