using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.Domain.Entities
{
    public class HoursAmount
    {
        public long Id { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }

    }
}
