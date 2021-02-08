using BillPay.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillPay.Domain.Interface.Repository
{
    /// <summary>
    /// Interface that implements a base repository.
    /// </summary>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Inserts the specified entidade.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Return the entity.</returns>
        T Insert(T entity);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return the entity.</returns>
        T GetById(string id);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>Return the instances.</returns>
        IQueryable<T> List();

        /// <summary>
        /// Updates the specified entidade.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(T entity);

        /// <summary>
        /// Gets the next identifier.
        /// </summary>
        /// <returns>Return de next identifier.</returns>
        string GetNextId();
    }
}
