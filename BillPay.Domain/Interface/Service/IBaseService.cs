using BillPay.Domain.Entity;
using System.Linq;

namespace BillPay.Domain.Interface.Service
{
    /// <summary>
    /// Interface that implements the base service.
    /// </summary>
    public interface IBaseService<T> where T : BaseEntity
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>Return the instance.</returns>
        IQueryable<T> List();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return the entity.</returns>
        T GetById(string id);

        /// <summary>
        /// Inserts the specified entidade.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Return the entity.</returns>
        void Insert(T entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(T entidade);

        /// <summary>
        /// Updates the specified entidade.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entidade);

        /// <summary>
        /// Gets the next identifier.
        /// </summary>
        /// <returns>Return de next identifier.</returns>
        string GetNextId();
    }
}
