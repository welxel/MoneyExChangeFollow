using AppCore.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities {
    public class Currency:RecordBase {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<CurrencyDetail> CurrencyDetail { get; set; }
    }
}
