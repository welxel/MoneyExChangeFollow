using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class CurrencyDetail:RecordBase
    {
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public double ChangesRound { get; set; }

        public int CurrencyId { get; set; }

        public Currency currencies { get; set; }
    }
}
