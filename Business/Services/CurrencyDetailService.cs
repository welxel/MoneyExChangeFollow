using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CurrencyDetailService : ICurrencyDetailService
    {
        private readonly ICurrencyDetailRepository _db;

        public CurrencyDetailService(ICurrencyDetailRepository db)
        {
            _db = db;
        }

        public Result Add(CurrencyDetailModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Result<IQueryable<CurrencyDetailModel>> GetJustDetail()
        {
            try
            {
                var result = _db.GetEntityQuery().Include(x => x.Currencies).Select(c => new CurrencyDetailModel()
                {
                    Date = c.Date,
                    Currency = c.Currency,
                    Changes = c.Changes,
                    Rate = c.Rate
                });
                return new SuccessResult<IQueryable<CurrencyDetailModel>>(result);
            }
            catch (Exception)
            {
                return new ErrorResult<IQueryable<CurrencyDetailModel>>("SomeThink is wrong");
            }
           
        }

        public Result<IQueryable<CurrencyDetailModel>> GetQuery()
        {
            try
            {
                var result = _db.GetEntityQuery().Include(x => x.Currencies).Select(c => new CurrencyDetailModel()
                {
                    Date = c.Date,
                    Currency = c.Currency,
                    Changes = c.Changes,
                    Rate = c.Rate,
                    Currencies = new CurrencyModel()
                    {
                        Currency = c.Currencies.Currency,
                        CurrentRate = c.Currencies.CurrentRate,
                        LastUpdate = c.Currencies.LastUpdate
                    },
                });

                return new SuccessResult<IQueryable<CurrencyDetailModel>>(result);

            }
            catch (Exception)
            {

                return new ErrorResult<IQueryable<CurrencyDetailModel>>("SomeThink is wrong");
            }
            
        }

        public Result Update(CurrencyDetailModel model)
        {
            throw new NotImplementedException();
        }
    }
}
