using Microsoft.AspNetCore.Mvc;
using BillPay.Domain.Entity;
using BillPay.Domain.Interface;
using System.Collections.Generic;
using System.Linq;
using BillPay.Domain.Interface.Service;
using BillPay.Domain.Validator;

namespace BillPay.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TService, TEntity>
        where TService : IBaseService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IBaseService<TEntity> _service;

        public BaseController(IBaseService<TEntity> servico)
        {
            _service = servico;
        }

        /// <summary>
        /// Lista todos os registros.
        /// </summary>
        /// <returns>Retorna a lista.</returns>
        [HttpGet]
        public virtual IEnumerable<TEntity> Get()
        {
            var lista = _service.List();

            if (lista.Count() <= 0) throw new NotFoundException();

            return lista;
        }

        /// <summary>
        /// Consulta a entidade por id.
        /// </summary>
        /// <param name="id">O id.</param>
        /// <returns>A entidade consultada.</returns>
        [HttpGet("{id}")]
        public virtual TEntity Get(string id)
        {
            var entidade = _service.GetById(id);

            if (entidade == null) throw new NotFoundException();

            return entidade;
        }

        /// <summary>
        /// Inclui uma entidade.
        /// </summary>
        /// <param name="entidade">A entidade a ser incluida.</param>
        /// <returns>Retorna a entidade incluida.</returns>
        [HttpPost]
        public virtual string Post([FromBody] TEntity entidade)
        {
            entidade.Id = _service.GetNextId();
            _service.Insert(entidade);
            return entidade.Id;
        }

        /// <summary>
        /// Altera uma entidade.
        /// </summary>
        /// <param name="id">O id da entidade a ser alterada.</param>
        /// <param name="entidade">A entidade a ser alterada.</param>
        /// <returns>A entidade alterada.</returns>
        [HttpPut("{id}")]
        public virtual string Put(string id, [FromBody] TEntity entidade)
        {
            if (string.IsNullOrEmpty(id)) throw new BusinessException<ResultValidator>("id", "Informe o id.");

            _service.Update(entidade);
            return entidade.Id;
        }

        /// <summary>
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="id">O id da entidade a ser excluida.</param>
        /// <returns>Retorno da operação da exclusão.</returns>
        [HttpDelete("{id}")]
        public virtual void Delete(string id)
        {
            var entidade = _service.GetById(id);
            if (entidade != null) _service.Remove(entidade);
        }
    }
}