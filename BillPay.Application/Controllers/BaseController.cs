using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Service;
using BillPay.Domain.Validator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BillPay.Application.Controllers
{
    /// <summary>
    /// Class that implements the base controller.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TService, TEntity>
        where TService : IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        #region variables
        /// <summary>
        /// The service.
        /// </summary>
        protected readonly IBaseService<TEntity> _service;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{TService, TEntity}"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BaseController(IBaseService<TEntity> service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Lists all records.
        /// </summary>
        /// <returns>Return the records.</returns>
        [HttpGet]
        public virtual IEnumerable<TEntity> Get()
        {
            var lista = _service.List();

            if (lista.Count() <= 0) throw new NotFoundException();

            return lista;
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return the entity.</returns>
        [HttpGet("{id}")]
        public virtual TEntity Get(string id)
        {
            var entidade = _service.GetById(id);

            if (entidade == null) throw new NotFoundException();

            return entidade;
        }

        /// <summary>
        /// Insert the specified entity.
        /// </summary>
        /// <param name="entidade">The entidade.</param>
        /// <returns>Return the id of entity.</returns>
        [HttpPost]
        public virtual string Post([FromBody] TEntity entidade)
        {
            entidade.Id = _service.GetNextId();
            _service.Insert(entidade);
            return entidade.Id;
        }

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <param name="entidade">The entity.</param>
        /// <returns>Return the id of entity.</returns>
        /// <exception cref="BusinessException{ResultValidator}">id - This field is required.</exception>
        [HttpPut]
        public virtual string Put([FromBody] TEntity entidade)
        {
            if (string.IsNullOrEmpty(entidade.Id)) throw new BusinessException<ResultValidator>("id", "This field is required.");

            _service.Update(entidade);
            return entidade.Id;
        }

        /// <summary>
        /// Delete the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public virtual void Delete(string id)
        {
            var entidade = _service.GetById(id);
            if (entidade != null) _service.Remove(entidade);
        }
        #endregion
    }
}