using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Data
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BillRepository(BaseContext context)
         : base(context)
        {
        }
    }
}
