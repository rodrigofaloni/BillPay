using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BillPay.Application.Controllers
{
    public class BillPayController : BaseController<IBillService, Bill>
    {
        private readonly IBillService _billService;
        private readonly IConfiguration _config;

        public BillPayController(IBillService billService, IConfiguration config) :
            base(billService)
        {
            _billService = billService;
            _config = config;
        }
    }
}