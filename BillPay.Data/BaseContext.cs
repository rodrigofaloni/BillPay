using BillPay.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BillPay.Data
{
    /// <summary>
    /// Classe that implements the base context.
    /// </summary>
    public class BaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BaseContext(DbContextOptions<BaseContext> options)
          : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the bills.
        /// </summary>
        /// <value>
        /// The bills.
        /// </value>
        public DbSet<Bill> Bills { get; set; }
    }
}
