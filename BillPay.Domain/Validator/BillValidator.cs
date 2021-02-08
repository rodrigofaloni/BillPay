using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Validator;
using BillPay.Domain.Validator.Base;
using FluentValidation;
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

        public override IBaseValidator<Bill> AddValidator()
        {
            this.CommonRules();
            return base.AddValidator();
        }

        private void CommonRules()
        {
            this.RequiredField(x => x.Name).When(x => x != null);
            this.RequiredField(x => x.PayDay).When(x => x != null);
            this.RequiredField(x => x.OriginalValue).When(x => x != null);
            this.RequiredField(x => x.ExpirationDate).When(x => x != null);
        }
    }
}
