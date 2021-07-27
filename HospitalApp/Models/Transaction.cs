﻿using System;
using System.Collections.Generic;

#nullable disable

namespace HospitalApp.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SerialNumberId { get; set; }
        public int AccountTreeId { get; set; }
        public decimal ValuDebit { get; set; }
        public decimal ValueCredit { get; set; }

        public virtual AccountsTree AccountsTree { get; set; }
        public virtual EntriesSerialize entriesSerialize { get; set; }
    }
}
