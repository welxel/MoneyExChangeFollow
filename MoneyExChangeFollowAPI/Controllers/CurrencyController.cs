using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyExChangeFollowAPI.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _service;
        private readonly ICurrencyDetailService _detailService;
        public CurrencyController(ICurrencyService service, ICurrencyDetailService detailService)
        {
            _service = service;
            _detailService = detailService;
        }

        [HttpGet("getcurrency")]
        public IActionResult GetCurrency()
        {
            var result =_service.GetQuery();
            if (result.Status==ResultStatus.Success)
            {   
                 return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetCurrencyCodeDetail")]
        public IActionResult GetCurrencyCodeDetail(CurrencyDetailRequest currencyDetailModel)
        {
            var result = _detailService.GetDetails();
            if (result.Status== ResultStatus.Success)
            {
                return Ok(new SuccessResult<List<CurrencyDetailModel>>(result.Data.Where(x => x.Currency == currencyDetailModel.Code).ToList()));
            }
            return BadRequest(result);
        }
    }
}
