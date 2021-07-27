using System;
using System.Collections.Generic;

#nullable disable

namespace HospitalApp.Models
{
    public partial class EntriesSerialize
    {
        public EntriesSerialize()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int Serial { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
