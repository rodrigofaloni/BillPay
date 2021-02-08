using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Validator;
using FluentValidation;
using System;
using System.Linq.Expressions;

namespace BillPay.Domain.Validator
{
    public abstract class BaseValidator<T> : AbstractValidator<T>, IBaseValidator<T> where T : BaseEntity
    {
        public virtual IBaseValidator<T> AddValidator()
        {
            return this;
        }

        public virtual IBaseValidator<T> ValidadorAtualizacao()
        {
            return this;
        }

        public virtual IBaseValidator<T> ValidadorRemocao()
        {
            return this;
        }

        public ResultValidator Validar(T objeto)
        {
            if (objeto == null)
            {
                return new ResultValidator(this.InconsistenciaObjetoVazio());
            }

            var inconsistencias = new ResultValidator();

            var validacaoFluent = this.Validate(objeto);

            if (!validacaoFluent.IsValid)
            {
                foreach (var erro in validacaoFluent.Errors)
                {
                    inconsistencias.AdicionarInconsistencia(new Inconsistency(erro.PropertyName, erro.ErrorMessage) { });
                }
            }

            return inconsistencias;
        }

        protected Inconsistency InconsistenciaObjetoVazio()
        {
            return new Inconsistency("Id", "Empty or not informed object.");
        }

        /// <summary>
        /// Regra básica para campo obrigátorio (not null).
        /// </summary>
        /// <typeparam name="TProperty">Tipo da propriedade para composição da regra.</typeparam>
        /// <param name="propriedade">A propriedade de fato para construção da regra.</param>
        /// <returns>RuleBuilder para continuação de possíveis regras.</returns>
        protected IRuleBuilderOptions<T, TProperty> RequiredField<TProperty>(Expression<Func<T, TProperty>> propriedade)
        {
            return RuleFor(propriedade)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("This field is required.")
                .NotEmpty().WithMessage("This field is required.");
        }
    }
}
