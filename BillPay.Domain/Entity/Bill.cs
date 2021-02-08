using System;
using System.Text.Json.Serialization;

namespace BillPay.Domain.Entity
{
    /// <summary>
    /// Class that implements the bill.
    /// </summary>
    public class Bill : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the original value.
        /// </summary>
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the pay day.
        /// </summary>
        public DateTime PayDay { get; set; }

        /// <summary>
        /// Gets the corrected value.
        /// </summary>
        [JsonIgnore]
        public double CorrectedValueMDL { get; set; }

        /// <summary>
        /// Gets the delayed days.
        /// </summary>
        [JsonIgnore]
        public int DelayedDaysMDL { get; set; }

        /// <summary>
        /// Gets or sets the calculation rule.
        /// </summary>
        [JsonIgnore]
        public string CalculationRule { get; set; }

        /// <summary>
        /// Gets the corrected value.
        /// </summary>
        public double CorrectedValue 
        {
            get { return this.CorrectedValueMDL; }
        }

        /// <summary>
        /// Gets the delayed days.
        /// </summary>
        public int DelayedDays 
        {
            get { return this.DelayedDaysMDL; }
        }
    }
}
