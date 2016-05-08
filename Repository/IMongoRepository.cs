using MongoDB.Driver;
using System;
using System.Linq;

namespace Repository
{
    public interface IMongoRepository
    {
        /// <summary>
        /// Reads the specified entity with given identifier.
        /// </summary>
        /// <typeparam name="T">Entity that implemnts IDocument</typeparam>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Document if document with specified id exists, null otherwise.</returns>
        T Read<T>(Guid entityId) where T : IDocument;

        /// <summary>
        /// Persists the specified entity. Use replace API with upsert option to create/update, to update specific properties or sub-documents 
        /// use <see cref="Update<T>"/> with update builders.
        /// </summary>
        /// <typeparam name="T">Entity that implemnts IDocument</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Saved Entity</returns>
        T Persist<T>(T entity) where T : IDocument;

        /// <summary>
        /// Deletes the specified entity with given identifier.
        /// </summary>
        /// <typeparam name="T">Entity that implemnts IDocument</typeparam>
        /// <param name="entityId">The entity identifier.</param>
        void Delete<T>(Guid entityId) where T : IDocument;

        /// <summary>
        /// Updates the specified entity identifier.
        /// </summary>
        /// <typeparam name="T">Entity that implemnts IDocument</typeparam>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="update">The update.</param>
        /// <returns>Update document</returns>
        T Update<T>(Guid entityId,UpdateDefinition<T> update) where T : IDocument;

        /// <summary>
        /// Queries the collection with specified filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">The filter for filtering search and sort and paging options.</param>
        /// <returns></returns>
        IQueryable<T> Query<T>(DocumentFilter<T> filter) where T : IDocument;
    }
}
