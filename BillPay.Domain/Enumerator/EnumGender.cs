using System.ComponentModel;

namespace BillPay.Domain.Enumerator
{
    /// <summary>
    /// Enumerator that defines the gender.
    /// </summary>
    public enum EnumGender
    {
        /// <summary>
        /// Define the gender male.
        /// </summary>
        [Description("MALE")]
        MALE,

        /// <summary>
        /// Define the gender female.
        /// </summary>
        [Description("FEMALE")]
        FEMALE,
    }
}
