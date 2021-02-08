using BillPay.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator.Base
{
    public class EntityValidator<T> : BaseValidator<T> where T : BaseEntity
    {
    }
}
