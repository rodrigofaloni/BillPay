using System;

namespace BillPay.Domain.Validator
{
    /// <summary>
    /// Class that implements the business exception.
    /// </summary>
    /// <typeparam name="TDetail">The type of the detail.</typeparam>
    public class BusinessException<TDetail> : Exception where TDetail : ResultValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException{TDetail}"/> class.
        /// </summary>
        /// <param name="result">The resultado.</param>
        public BusinessException(TDetail result)
        {
            this.Details = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException{TDetail}"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessException(string message) : this(null, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException{TDetail}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="message">The message.</param>
        public BusinessException(string property, string message)
        {
            var detail = new ResultValidator();
            detail.AddInconsistency(new Inconsistency(property, message));
            Details = (TDetail)detail;
        }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public TDetail Details { get; }
    }
}
