using System;

namespace BillPay.Domain.Validator
{
    /// <summary>
    /// Class that implements the inconsistency.
    /// </summary>
    [Serializable]
    public class Inconsistency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Inconsistency"/> class.
        /// </summary>
        /// <param name="validateProperty">The validate property.</param>
        /// <param name="message">The message.</param>
        public Inconsistency(string validateProperty, string message)
        {
            this.ValidateProperty = validateProperty;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the validate property.
        /// </summary>
        /// <value>
        /// The validate property.
        /// </value>
        public virtual string ValidateProperty { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public virtual string Message { get; set; }
    }
}
