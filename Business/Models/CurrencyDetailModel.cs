using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class CurrencyDetailModel:RecordBase
    {
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public double ChangesRound { get; set; }

        public int CurrencyId { get; set; }

        public CurrencyModel currencies { get; set; }
    }
}
