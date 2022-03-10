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
        public void Dispose()
        {
            _db.Dispose();
        }

        public  bool FillCurrentInfo()
        {
            var response =  _integrationService.GetAsync("https://www.tcmb.gov.tr/kurlar/today.xml").GetAwaiter().GetResult();
            string xmlBody =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            XmlSerializer serializer = new XmlSerializer(typeof(CurrencyDateXML));
            using (StringReader reader = new StringReader(xmlBody))
            {
                var xmlFormat = (CurrencyDateXML)serializer.Deserialize(reader);
                var xmlDate =FormatingDateTimeNow(Convert.ToDateTime(xmlFormat.Date));

                var codeList = _db.GetEntityQuery().ToList();

                foreach (var item in xmlFormat.Currency)
                {
                    if (item.CurrencyCode == "USD" || item.CurrencyCode == "EUR" || item.CurrencyCode == "GBP" || item.CurrencyCode == "CHF" || item.CurrencyCode == "KWD" || item.CurrencyCode == "SAR" || item.CurrencyCode == "RUB")
                    {
                        foreach (var currencies in codeList)
                        { 
                            if (currencies.LastUpdate<xmlDate)
                            {
                                var currency = codeList.Where(x => x.Id == currencies.Id).FirstOrDefault();
                                currency.Currency = currencies.Currency;
                                currency.CurrentRate = currencies.CurrentRate;
                                currency.LastUpdate = xmlDate;
                                _db.Update(currency);
                            }
                        }

                        if (codeList.Where(x => x.Currency == item.CurrencyCode).Count() == 0)
                        {
                            Currencies currency = new Currencies()
                            {
                                Currency = item.CurrencyCode,
                                CurrentRate = double.Parse(item.ForexBuying, System.Globalization.CultureInfo.InvariantCulture),
                                LastUpdate = xmlDate,
                                CurrencyDetail = new List<CurrencyDetail>() { new CurrencyDetail() {
                            Date=xmlDate,
                            Currency=item.CurrencyCode,
                            Rate=double.Parse(item.ForexBuying, System.Globalization.CultureInfo.InvariantCulture),
                            Changes="-"
                            } }
                            };
                            _db.Add(currency);
                        }

                        else
                        {
                            CurrencyDetailModel lastCurrency = null;
                            CurrencyDetailModel todayCurrency = null;
                            double round = 0;
                            var currencyDetailList = _detailService.GetQuery().Data;
                            lastCurrency = currencyDetailList.Where(x => x.Date == xmlDate.AddDays(-1)&& x.Currencies.Currency == item.Kod).FirstOrDefault();
                            todayCurrency= currencyDetailList.Where(x => x.Date == xmlDate&& x.Currencies.Currency == item.Kod).FirstOrDefault();
                            if (lastCurrency != null && todayCurrency == null)
                            {
                                round= ((double.Parse(item.ForexBuying, System.Globalization.CultureInfo.InvariantCulture) - lastCurrency.Rate) / lastCurrency.Rate) * 100;
                                var curreny = codeList.Where(x => x.Currency == item.CurrencyCode).FirstOrDefault();
                                var detail = new CurrencyDetail()
                                {
                                    Currency = curreny.Currency,
                                    Rate = double.Parse(item.ForexBuying, System.Globalization.CultureInfo.InvariantCulture),
                                    Changes = ChangesText(round),
                                    Date = xmlDate,
                                    Currencies = curreny
                                };
                                _detailDb.Add(detail);
                            }
                        }
                    
                    }
                }

            }
            return true;
        }

        public Result<IQueryable<CurrencyModel>> GetQuery()
        {
            var result = _db.GetEntityQuery().Include(x => x.CurrencyDetail).Select(x => new CurrencyModel() {
                Currency = x.Currency,
                CurrentRate = x.CurrentRate,
                LastUpdate=x.LastUpdate,
            }).OrderBy(x=>x.Currency).ThenBy(x=>x.CurrentRate);

            return new SuccessResult<IQueryable<CurrencyModel>>(result);
        }

        private DateTime FormatingDateTimeNow(DateTime date)
        {
            var formatDate = date.ToString("MM/dd/yyyy 00:00:00");
            return DateTime.Parse(formatDate);
        }
        private String ChangesText(double number)
        {
            if (number<0)
            {
                return Math.Round(number, 2)  + "%";
            }
            else if (number>0)
            {
                return "+" + Math.Round(number, 2) + "%";
            }
            else
            {
                return "-";
            }
        }
    }
}

