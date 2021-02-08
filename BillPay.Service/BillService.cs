using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Service;
using BillPay.Domain.Interface.Validator;
using BillPay.Domain.Validator;
using BillPay.Domain.Validator.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Service
{
    public class BillService : BaseService<Bill>, IBillService
    {
        /// <summary>
        /// The contact repository.
        /// </summary>
        private IBillRepository billRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BillService"/> class.
        /// </summary>
        /// <param name="billRepository">The bill repository.</param>
        public BillService(IBillRepository billRepository)
            : base(billRepository)
        {
            this.billRepository = billRepository;
        }

        protected override IBaseValidator<Bill> Validador
        {
            get
            {
                return new BillValidator(this.billRepository);
            }
        }
    }
}
