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

        public Result<IQueryable<CurrencyDetailModel>> GetQuery()
        {
            try
            {
                var result = _db.GetEntityQuery().Include(x => x.currencies).Select(c => new CurrencyDetailModel()
                {
                    Date = c.Date,
                    Id = c.Id,
                    ChangesRound = c.ChangesRound,
                    Rate = c.Rate,
                    currencies = new CurrencyModel()
                    {
                        Id = c.currencies.Id,
                        Code = c.currencies.Code,
                        Name = c.currencies.Name
                    },
                    CurrencyId = c.CurrencyId,
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
