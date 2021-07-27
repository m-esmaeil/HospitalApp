using System;
using System.Collections.Generic;

#nullable disable

namespace HospitalApp.Models
{
    public partial class AccountsTree
    {
        public AccountsTree()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? FollowTo { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Categories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }

}
