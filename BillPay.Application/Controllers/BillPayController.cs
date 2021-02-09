using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Service;
using Microsoft.Extensions.Configuration;

namespace BillPay.Application.Controllers
{
    /// <summary>
    /// Class that implements the bill and pay controller.
    /// </summary>
    public class BillPayController : BaseController<IBillService, Bill>
    {
        #region Variables
        /// <summary>
        /// The bill service.
        /// </summary>
        private readonly IBillService _billService;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _config;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BillPayController"/> class.
        /// </summary>
        /// <param name="billService">The bill service.</param>
        /// <param name="config">The configuration.</param>
        public BillPayController(IBillService billService, IConfiguration config) :
            base(billService)
        {
            _billService = billService;
            _config = config;
        }
        #endregion
    }
}