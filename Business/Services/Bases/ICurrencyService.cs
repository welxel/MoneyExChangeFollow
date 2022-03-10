using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Bases {
    public interface ICurrencyService:IService<CurrencyModel> {
        bool FillCurrentInfo();
    }
}
