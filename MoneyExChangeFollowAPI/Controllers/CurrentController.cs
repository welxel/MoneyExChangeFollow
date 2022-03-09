using AppCore.Business.Models.Results;
using Business.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentController : ControllerBase
    {
        private readonly ICurrencyService _service;
        public CurrentController(ICurrencyService service)
        {
            _service = service;
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
    }
}
