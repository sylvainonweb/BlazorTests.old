using System;
using System.Collections.Generic;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System.Reflection;
using SD.LLBLGen.Pro.QuerySpec.Adapter;
using BlazorTests.Common.Technical.Debug;
using BlazorTests.Common.Data.Exceptions;
using System.Threading.Tasks;

namespace BlazorTests.Common.Technical.Database
{
    public class Repository : IRepository
    {
        #region Propriétés

        public string ConnectionString
        {
            get
            {
                return adapter.ConnectionString;
            }
            set
            {
                adapter.ConnectionString = value;
            }
        }

        public bool MustLogQueriesForSynchronization
        {
            get
            {
                return adapter.MustLogQueriesForSynchronization;
            }
            set
            {
                adapter.MustLogQueriesForSynchronization = value;
            }
        }        

        public int CommandTimeOut
        {
            get
            {
                return adapter.CommandTimeOut;
            }
            set
            {
                adapter.CommandTimeOut = value;
            }
        }

        public string User
        {
            get
            {
                return adapter.User;
            }
            set
            {
                adapter.User = value;
            }
        }

        public IList<string> FilesToDeleteOnCommit { get; }
        public IList<IEntityCollection2> RemovedEntitiesTrackerCollectionsToClearOnCommit { get; }

        protected IDataAccessAdapterEx adapter;
        protected FunctionMappingStore functionMappingStore;

        private const string MessageSuppressionImpossible = "Suppression impossible car cette donnée est utilisée ailleurs.";

        #endregion

        #region Initialisation

        public Repository(IDataAccessAdapterEx adapter, FunctionMappingStore functionMappingStore)
        {
            this.adapter = adapter;
            this.functionMappingStore = functionMappingStore;

            this.FilesToDeleteOnCommit = new List<string>();
            this.RemovedEntitiesTrackerCollectionsToClearOnCommit = new List<IEntityCollection2>();
        }

        #endregion

        #region Récupération de données

        /// <summary>
        /// Retourne l'entité correspondant au type spécifié.
        /// Renvoie une exception si l'entité n'existe pas.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get<T,U>(U id) where T : EntityBase2
        {
            return Get<T,U>(id, null, true);
        }

        /// <summary>
        /// Retourne l'entité correspondant au type spécifié.
        /// Utilise la réflexion pour trouver le constructeur de l'entité prenant l'identifiant en paramètre
        /// </summary>
        /// <typeparam name="T">Le type de l'entité à retourner</typeparam>
        /// <typeparam name="U">le type de la clé primaire de l'entité</typeparam>
        /// <param name="id"></param>
        /// <param name="throwExceptionIfNotExists"></param>
        /// <returns></returns>
        public T Get<T,U>(U id, PrefetchPath2 prefetchPath, bool throwExceptionIfNotExists) where T : EntityBase2
        {
            Type[] types = new Type[1];
            types[0] = typeof(U);
            ConstructorInfo info = typeof(T).GetConstructor(types);

            EntityBase2 entity = info.Invoke(new object[1] { id }) as EntityBase2;
            AssertHelper.ObjectNotNull(entity, "Unable to find a constructor with one parameter");

            if (prefetchPath == null)
            {
                adapter.FetchEntity((T)entity);
            }
            else
            {
                adapter.FetchEntity((T)entity, prefetchPath);
            }

            if (throwExceptionIfNotExists)
            {
                // Mieux vaut prévenir que guérir. Afin d'éviter les exceptions du type "ORMEntityOutOfSyncException",
                // on vérifie que l'entité n'a pas été supprimé. Si c'est le cas, on envoie une exception à nous.
                if (((T)entity).Fields.State == EntityState.OutOfSync)
                {
                    throw new ApplicationException("Impossible de charger la donnée spécifiée");
                    //throw new UnableToLoadDataBecauseDataDoesntExistException();
                }
            }
            else
            {
                if (entity.IsNew)
                {
                    // Si IsNew=true, c'est que l'entité n'a pas été trouvé.
                    return null;
                }
            }

            return (T)entity;
        }

        public TEntity FetchSingle<TEntity>(EntityQuery<TEntity> query)
                  where TEntity : EntityBase2, IEntity2
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchSingle(query);
        }

        public TEntity FetchFirst<TEntity>(EntityQuery<TEntity> query) where TEntity : EntityBase2, IEntity2
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchFirst(query);
        }

        public IEntityCollection2 FetchQuery<TEntity>(EntityQuery<TEntity> query)
            where TEntity : IEntity2
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchQuery(query);
        }

        public TCollection FetchQuery<TEntity, TCollection>(EntityQuery<TEntity> query, TCollection collectionToFill)
            where TEntity : IEntity2
            where TCollection : IEntityCollection2
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchQuery(query, collectionToFill);
        }

        public IList<TElement> FetchQuery<TElement>(DynamicQuery<TElement> query)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchQuery(query);
        }

        public IList<object[]> FetchQuery(DynamicQuery query)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchQuery(query);
        }

        public TElement FetchSingle<TElement>(DynamicQuery<TElement> query)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchSingle(query);
        }

        public TElement FetchFirst<TElement>(DynamicQuery<TElement> query)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchFirst(query);
        }

        public TValue FetchScalar<TValue>(DynamicQuery query)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            return adapter.FetchScalar<TValue>(query);
        }

        public void FetchEntityCollection(IEntityCollection2 collectionToFill, IRelationPredicateBucket filterBucket, int maxNumberOfItemsToReturn, ISortExpression sortClauses)
        {
            adapter.FetchEntityCollection(collectionToFill, filterBucket, maxNumberOfItemsToReturn, sortClauses);
        }

        #endregion

        #region Sauvegarde

        /// <summary>
        /// Sauvegarde une entité dans la base de données
        /// </summary>
        /// <typeparam name="T">Le type de l'entité</typeparam>
        /// <param name="entity">l'entité elle même</param>
        public virtual void Save<T>(T entity) where T : EntityBase2
        {
            Save(entity, true);
        }

        //protected abstract IConcurrencyPredicateFactory CreateConcurrencyPredicateFactory();
        public virtual void Save<T>(T entity, bool refetchAfterSave = true) where T : EntityBase2
        {
            //IConcurrencyPredicateFactory concurrencyFactory = CreateConcurrencyPredicateFactory();
            //IPredicateExpression predicate = concurrencyFactory.CreatePredicate(ConcurrencyPredicateType.Save, entity);
            //adapter.SaveEntity(entity, refetchAfterSave, predicate, true);

            adapter.SaveEntity(entity, refetchAfterSave, true);
        }

        /// <summary>
        /// Permet d'effectuer un UPDATE sur un ensemble de données sans avoir à charger les entités en mémoire
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityWithNewValues">Une entité contenant les nouvelles valeurs à affecter à toutes les entités correspondant au filtre</param>
        /// <param name="bucket">le filtre</param>
        public virtual void UpdateDirectly<T>(T entityWithNewValues, IRelationPredicateBucket bucket) where T : EntityBase2
        {
            adapter.UpdateEntitiesDirectly(entityWithNewValues, bucket);
        }

        #endregion

        #region Suppression

        /// <summary>
        /// Supprime l'entité passée en paramètre de la base de données
        /// </summary>
        /// <typeparam name="T">Le type de l'entité</typeparam>
        /// <param name="entity">l'entité à supprimer</param>
        public void Delete<T>(T entity) where T : EntityBase2
        {
            try
            {
                adapter.DeleteEntity(entity);
            }
            catch(Exception e)
            {
                string s = e.ToString();
                throw new DlmBusinessException(MessageSuppressionImpossible);
            }
        }

        /// <summary>
        /// Supprime la collection d'entités passé en paramètre
        /// </summary>
        /// <param name="collection"></param>
        public void Delete(IEntityCollection2 collection)
        {
            // La prorpriété RemovedEntitiesTracker des collections n'étant pas toujours renseignée, je préfère faire un test pour que cela ne plante pas
            // plutôt que :
            // 1) d'être obligé de faire ce test à chaque appel
            // 2) obligé de renseigner les propriétés RemovedEntitiesTracker de toutes les collections à chaque récupération d'une entité
            if (collection != null)
            {
                try
                {
                    this.adapter.DeleteEntityCollection(collection);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(MessageSuppressionImpossible, e);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeOfEntity"></param>
        /// <param name="filterBucket"></param>
        /// <returns></returns>
        public int DeleteEntitiesDirectly(Type typeOfEntity, IRelationPredicateBucket filterBucket)
        {
            try
            { 
                return this.adapter.DeleteEntitiesDirectly(typeOfEntity, filterBucket);
            }
            catch (Exception e)
            {
                throw new ApplicationException(MessageSuppressionImpossible, e);
            }
        }

        #endregion

        #region Transaction

        public void StartTransaction()
        {
            adapter.StartTransaction(System.Data.IsolationLevel.ReadCommitted, "Transaction");
        }

        public void Commit()
        {
            adapter.Commit();
        }

        public void Rollback()
        {
            adapter.Rollback();
        }

        #endregion

        #region Exécution de "Raw SQL"

        public void ExecuteSQL(string rawSQL)
        {
            this.adapter.ExecuteSQL(rawSQL);
        }

        public Task ExecuteSQLAsync(string rawSQL)
        {
            return this.adapter.ExecuteSQLAsync(rawSQL);
        }
        #endregion

        public int Count(DynamicQuery query, DynamicQuery intermediateQuery)
        {
            query.CustomFunctionMappingStore = functionMappingStore;
            intermediateQuery.CustomFunctionMappingStore = functionMappingStore;
            var result = adapter.FetchScalar<int?>(intermediateQuery.Select(query.CountRow()));
            if (result.HasValue)
            {
                return result.Value;
            }

            return 0;
        }
    }
}
