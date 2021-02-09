using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Service;
using BillPay.Domain.Interface.Validator;
using BillPay.Domain.Validator;

namespace BillPay.Service
{
    /// <summary>
    /// Class that implements the bill service.
    /// </summary>
    public class BillService : BaseService<Bill>, IBillService
    {
        /// <summary>
        /// The bill repository.
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

        /// <summary>
        /// Gets the validador.
        /// </summary>
        /// <value>
        /// The validador.
        /// </value>
        protected override IBaseValidator<Bill> Validador
        {
            get
            {
                return new BillValidator(this.billRepository);
            }
        }

        /// <summary>
        /// Internals the insert.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected override void InternalInsert(Bill entity)
        {
            GetCorrectedValues(entity);
            base.InternalInsert(entity);
        }

        /// <summary>
        /// Internals the update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected override void InternalUpdate(Bill entity)
        {
            GetCorrectedValues(entity);
            base.InternalUpdate(entity);
        }

        /// <summary>
        /// Gets the corrected values.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private static void GetCorrectedValues(Bill entity)
        {
            if(entity.OriginalValue > 0)
            {
                var days = (int)entity.PayDay.Subtract(entity.ExpirationDate).TotalDays;
                double fine = 0;
                double dailyInterest = 0;
                var rule = days > 0 ? "{0} + ({0} * ({3}/100)) + ({0} * ({1}/100) * {2})" : string.Empty;
                entity.DelayedDaysMDL = days > 0 ? days : 0;

                if (entity.DelayedDays > 0 && entity.DelayedDays <= 3)
                {
                    fine = (double)entity.OriginalValue * (2D / 100);
                    dailyInterest = (double)entity.OriginalValue * ((0.1 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.1, days, 2);
                }
                else if (entity.DelayedDays > 3 && entity.DelayedDays <= 5)
                {
                    fine = (double)entity.OriginalValue * (3D / 100);
                    dailyInterest = (double)entity.OriginalValue * ((0.2 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.2, days, 3);
                }
                else if (entity.DelayedDays > 5)
                {
                    fine = (double)entity.OriginalValue * (5D / 100);
                    dailyInterest = (double)entity.OriginalValue * ((0.3 / 100) * days);
                    rule = string.Format(rule, entity.OriginalValue, 0.3, days, 5);
                }

                entity.CorrectedValueMDL = (double)entity.OriginalValue + fine + dailyInterest;
                entity.CalculationRule = rule;
            }
        }
    }
}
