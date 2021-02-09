using System;
using System.Collections.Generic;
using System.Linq;

namespace BillPay.Domain.Validator
{
    /// <summary>
    /// Class that implements the result validator.
    /// </summary>
    [Serializable]
    public class ResultValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultValidator"/> class.
        /// </summary>
        public ResultValidator()
        {
            this.InconsistencyList = null;
            this.ResultValid = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultValidator"/> class.
        /// </summary>
        /// <param name="inconsistency">The inconsistency.</param>
        public ResultValidator(Inconsistency inconsistency)
        {
            this.AddInconsistency(inconsistency);
            this.ResultValid = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [result valid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [result valid]; otherwise, <c>false</c>.
        /// </value>
        protected bool ResultValid { get; set; }

        /// <summary>
        /// Gets or sets the inconsistency list.
        /// </summary>
        /// <value>
        /// The inconsistency list.
        /// </value>
        protected List<Inconsistency> InconsistencyList { get; set; }

        /// <summary>
        /// Adds the inconsistency.
        /// </summary>
        /// <param name="inconsistency">The inconsistency.</param>
        public void AddInconsistency(Inconsistency inconsistency)
        {
            if (this.InconsistencyList == null)
            {
                this.InconsistencyList = new List<Inconsistency>();
            }

            this.InconsistencyList.Add(inconsistency);
            this.ResultValid = false;
        }

        /// <summary>
        /// Gets the inconsistencies.
        /// </summary>
        /// <returns>Return the inconsistencies.</returns>
        public List<Inconsistency> GetInconsistencies()
        {
            return this.InconsistencyList.ToList();
        }

        /// <summary>
        /// Results is valid.
        /// </summary>
        /// <returns>Return the result is valid.</returns>
        public bool ResultIsValid()
        {
            return this.ResultValid;
        }
    }
}
