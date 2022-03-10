using Business.Services.Bases;
using Microsoft.VisualBasic.CompilerServices;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI.Tasking
{
    public class FillCurrentDetail : IJob
    {
        private readonly ICurrencyService _service;
        public FillCurrentDetail(ICurrencyService service)
        {

            _service = service;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var task= MoneyExChangeFollowAPI.Extentions.Utils.Retry<bool>(() => {return _service.FillCurrentInfo(); }, 10, 30000); ;
            return task;
        }


    }
}


