using System.Collections.Generic;
using System.Net;

namespace BillPay.Domain.Entity
{
    /// <summary>
    /// Class that implements the controlled return.
    /// </summary>
    public class ControlledReturn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlledReturn"/> class.
        /// </summary>
        public ControlledReturn()
        {
            this.Inconsistencies = new List<InconsistencyModel>();
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the inconsistencies.
        /// </summary>
        /// <value>
        /// The inconsistencies.
        /// </value>
        public List<InconsistencyModel> Inconsistencies { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object Data { get; set; }
    }
}
