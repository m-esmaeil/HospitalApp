using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace HospitalApp.Models
{
    public partial class AccountsTree
    {
        public AccountsTree()
        {
            Transactions = new HashSet<Transaction>();
            //Children = new HashSet<AccountsTree>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Parent")]
        public int? FollowTo { get; set; }
        
        public virtual AccountsTree Parent { get; set; }
        //public virtual ICollection<AccountsTree> Children { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Categories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }

}
