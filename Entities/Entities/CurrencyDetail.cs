using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class CurrencyDetail:RecordBase
    {
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public string Changes { get; set; }

        public int CurrencyId { get; set; }

        public Currencies Currencies { get; set; }
    }
}
