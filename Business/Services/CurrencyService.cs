using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xml2CSharp;

namespace Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _db;
        private readonly ICurrencyDetailRepository _detailDb;
        private readonly ICurrencyDetailService _detailService;

        private HttpClient _integrationService;
        public CurrencyService(ICurrencyRepository db, ICurrencyDetailService detailService, ICurrencyDetailRepository detailDb)
        {
            _integrationService = new HttpClient();
            _detailService = detailService;
            _detailDb = detailDb;
            _db = db;
        }
        public Result Add(CurrencyModel model)
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

        public async Task FillAgainCurrentInfo()
        {
            var response = await _integrationService.GetAsync("https://www.tcmb.gov.tr/kurlar/today.xml");
            string xmlBody = await response.Content.ReadAsStringAsync();

            XmlSerializer serializer = new XmlSerializer(typeof(TarihDate));
            using (StringReader reader = new StringReader(xmlBody))
            {
                var xmlFormat = (TarihDate)serializer.Deserialize(reader);
                var xmlDate = Convert.ToDateTime(xmlFormat.Date);

                var codeList = _db.GetEntityQuery().ToList();

                foreach (var item in xmlFormat.Currency)
                {
                    if (item.CurrencyCode == "USD" || item.CurrencyCode == "EUR" || item.CurrencyCode == "GBP" || item.CurrencyCode == "CHF" || item.CurrencyCode == "KWD" || item.CurrencyCode == "SAR" || item.CurrencyCode == "RUB")
                    {
                        if (codeList.Where(x => x.Code == item.CurrencyCode).Count() == 0)
                        {
                            Currency currency = new Currency()
                            {
                                Code = item.CurrencyCode,
                                Name = item.CurrencyName,
                                CurrencyDetail = new List<CurrencyDetail>() { new CurrencyDetail() {
                            Date=xmlDate,
                            ChangesRound=Convert.ToDouble(item.ForexBuying),
                            Rate=0
                            } }
                            };
                            _db.Add(currency);
                        }
                        else
                        {
                            CurrencyDetailModel deneme = null;
                            double round = 0;
                            deneme = _detailService.GetQuery().Data.Where(x => x.Date == DateTime.Now.AddDays(-1) && x.currencies.Code == item.Kod).FirstOrDefault();
                            if (deneme !=null)
                            {
                                round= (( Convert.ToDouble(item.ForexBuying)- deneme.ChangesRound) / deneme.ChangesRound) * 100;
                            }

                            var curreny = codeList.Where(x => x.Code == item.CurrencyCode).FirstOrDefault();
                            var detail = new CurrencyDetail()
                            {
                                CurrencyId = curreny.Id,
                                ChangesRound = Convert.ToDouble(item.ForexBuying),
                                Rate = round,
                                Date = DateTime.Now
                            };
                            _detailDb.Add(detail);
                        }
                    
                    }
                }

            }
        }

        public Result<IQueryable<CurrencyModel>> GetQuery()
        {
            var result = _db.GetEntityQuery().Include(x => x.CurrencyDetail).Select(x => new CurrencyModel() {
                Code = x.Code,
                Name = x.Name,
                CurrencyDetail = x.CurrencyDetail
            }).OrderBy(x=>x.Code);

            return new SuccessResult<IQueryable<CurrencyModel>>(result);
        }

        public Result Update(CurrencyModel model)
        {
            throw new NotImplementedException();
        }
    }
}
