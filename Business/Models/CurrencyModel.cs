using AppCore.Records.Bases;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models {
    public class CurrencyModel:RecordBase {
        public string Currency { get; set; }
        public DateTime LastUpdate { get; set; }
        public double CurrentRate { get; set; }

        public List<CurrencyDetail> CurrencyDetail { get; set; }
    }
}
