using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;

namespace BillPay.Data
{
    /// <summary>
    /// Class that implements the bill repository.
    /// </summary>
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BillRepository(BaseContext context)
         : base(context)
        {
        }
    }
}
