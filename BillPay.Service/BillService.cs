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

        protected override void InternalInsert(Bill entity)
        {
            GetMoneyValues(entity);
            base.InternalInsert(entity);
        }

        protected override void InternalUpdate(Bill entity)
        {
            GetMoneyValues(entity);
            base.InternalUpdate(entity);
        }

        private static void GetMoneyValues(Bill entity)
        {
            if(entity.OriginalValue > 0)
            {
                var days = (int)entity.PayDay.Subtract(entity.ExpirationDate).TotalDays;
                double multa = 0;
                double jurosDiarios = 0;
                var rule = days > 0 ? "{0} + ({0} * ({3}/100)) + ({0} * ({1}/100) * {2})" : string.Empty;
                entity.DelayedDaysMDL = days > 0 ? days : 0;

                if (entity.DelayedDays > 0 && entity.DelayedDays <= 3)
                {
                    multa = (double)entity.OriginalValue * (2D / 100);
                    jurosDiarios = (double)entity.OriginalValue * ((0.1 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.1, days, 2);
                }
                else if (entity.DelayedDays > 3 && entity.DelayedDays <= 5)
                {
                    multa = (double)entity.OriginalValue * (3D / 100);
                    jurosDiarios = (double)entity.OriginalValue * ((0.2 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.2, days, 3);
                }
                else if (entity.DelayedDays > 5)
                {
                    multa = (double)entity.OriginalValue * (5D / 100);
                    jurosDiarios = (double)entity.OriginalValue * ((0.3 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.3, days, 5);
                }

                entity.CorrectedValueMDL = (double)entity.OriginalValue + multa + jurosDiarios;
                entity.CalculationRule = rule;
            }
        }
    }
}
