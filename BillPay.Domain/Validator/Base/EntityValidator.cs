using BillPay.Domain.Entity;

namespace BillPay.Domain.Validator.Base
{
    /// <summary>
    /// Class thats implements the entity validator.
    /// </summary>
    public class EntityValidator<T> : BaseValidator<T> where T : BaseEntity
    {
    }
}
