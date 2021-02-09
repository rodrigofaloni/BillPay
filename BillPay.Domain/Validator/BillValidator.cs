using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Validator;
using BillPay.Domain.Validator.Base;
using FluentValidation;

namespace BillPay.Domain.Validator
{
    /// <summary>
    /// Class that implements the bill validator.
    /// </summary>
    public class BillValidator : EntityValidator<Bill>
    {
        /// <summary>
        /// The bill repository
        /// </summary>
        private IBillRepository billRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BillValidator"/> class.
        /// </summary>
        /// <param name="billRepository">The bill repository.</param>
        public BillValidator(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }

        /// <summary>
        /// Adds the validator.
        /// </summary>
        /// <returns>
        /// Return the result of validator
        /// </returns>
        public override IBaseValidator<Bill> AddValidator()
        {
            this.CommonRules();
            return base.AddValidator();
        }

        /// <summary>
        /// Updates the validator.
        /// </summary>
        /// <returns>
        /// Return the result of validator
        /// </returns>
        public override IBaseValidator<Bill> UpdateValidator()
        {
            this.CommonRules();
            return base.UpdateValidator();
        }

        /// <summary>
        /// The commons rules.
        /// </summary>
        private void CommonRules()
        {
            this.RequiredField(x => x.Name).When(x => x != null);
            this.RequiredField(x => x.PayDay).When(x => x != null);
            this.RequiredField(x => x.OriginalValue).When(x => x != null);
            this.RequiredField(x => x.ExpirationDate).When(x => x != null);
        }
    }
}
