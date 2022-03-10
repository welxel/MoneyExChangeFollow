using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities {
    public class Currencies:RecordBase {
        public string Currency { get; set; }
        public DateTime LastUpdate { get; set; }
        public double CurrentRate { get; set; }

        public List<CurrencyDetail> CurrencyDetail { get; set; }
    }
}
