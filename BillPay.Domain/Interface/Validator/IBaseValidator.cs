using BillPay.Domain.Entity;
using BillPay.Domain.Validator;

namespace BillPay.Domain.Interface.Validator
{
    /// <summary>
    /// Interface thats implements the base validator.
    /// </summary>
    public interface IBaseValidator<T> where T : BaseEntity
    {
        /// <summary>
        /// Validates the specified objeto.
        /// </summary>
        /// <param name="objectItem">The object.</param>
        /// <returns>Return the result of validator</returns>
        ResultValidator ValidateMethod(T objectItem);

        /// <summary>
        /// Adds the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        IBaseValidator<T> AddValidator();

        /// <summary>
        /// Updates the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        IBaseValidator<T> UpdateValidator();

        /// <summary>
        /// Deletes the validator.
        /// </summary>
        /// <returns>Return the result of validator</returns>
        IBaseValidator<T> DeleteValidator();
    }
}
