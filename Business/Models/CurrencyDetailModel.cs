using AppCore.Records.Bases;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class CurrencyDetailModel:RecordBase
    {
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public string Changes { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int CurrencyId { get; set; }

        public CurrencyModel Currencies { get; set; }
    }
}
