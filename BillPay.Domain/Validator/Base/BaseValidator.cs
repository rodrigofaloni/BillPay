using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Validator;
using FluentValidation;
using System;
using System.Linq.Expressions;

namespace BillPay.Domain.Validator
{
    /// <summary>
    /// Class thats implements the base validator.
    /// </summary>
    public abstract class BaseValidator<T> : AbstractValidator<T>, IBaseValidator<T> where T : BaseEntity
    {
        /// <summary>
        /// Adds the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        public virtual IBaseValidator<T> AddValidator()
        {
            return this;
        }

        /// <summary>
        /// Updates the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        public virtual IBaseValidator<T> UpdateValidator()
        {
            return this;
        }

        /// <summary>
        /// Deletes the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        public virtual IBaseValidator<T> DeleteValidator()
        {
            return this;
        }

        /// <summary>
        /// Validates the specified objeto.
        /// </summary>
        /// <param name="objectItem">The object.</param>
        /// <returns>Return the result of validator</returns>
        public ResultValidator ValidateMethod(T objeto)
        {
            if (objeto == null)
            {
                return new ResultValidator(this.EmptyObjectInconsistency());
            }

            var inconsistencies = new ResultValidator();

            var validationFluent = this.Validate(objeto);

            if (!validationFluent.IsValid)
            {
                foreach (var error in validationFluent.Errors)
                {
                    inconsistencies.AddInconsistency(new Inconsistency(error.PropertyName, error.ErrorMessage) { });
                }
            }

            return inconsistencies;
        }

        /// <summary>
        /// Empties the object inconsistency.
        /// </summary>
        /// <returns>Return the inconsistency.</returns>
        protected Inconsistency EmptyObjectInconsistency()
        {
            return new Inconsistency("Id", "Empty or not informed object.");
        }

        /// <summary>
        /// Requireds the field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propriedade">The propriedade.</param>
        /// <returns>Return the result of validation.</returns>
        protected IRuleBuilderOptions<T, TProperty> RequiredField<TProperty>(Expression<Func<T, TProperty>> propriedade)
        {
            return RuleFor(propriedade)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("This field is required.")
                .NotEmpty().WithMessage("This field is required.");
        }
    }
}
