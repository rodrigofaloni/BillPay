using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Validator.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator
{
    public class BillValidator : EntityValidator<Bill>
    {
        private IBillRepository billRepository;

        public BillValidator(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }
    }
}
