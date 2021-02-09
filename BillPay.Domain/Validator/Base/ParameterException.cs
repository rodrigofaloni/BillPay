using System;

namespace BillPay.Domain.Validator.Base
{
    /// <summary>
    /// Class that implements the parameter exception.
    /// </summary>
    public class ParameterException : Exception
    {
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public string Property { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterException"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="message">The message.</param>
        public ParameterException(string property, string message) : base(message)
        {
            this.Property = property;
        }
    }
}
