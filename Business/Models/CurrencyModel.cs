using AppCore.Records.Bases;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models {
    public class CurrencyModel:RecordBase {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<CurrencyDetail> CurrencyDetail { get; set; }
    }
}
