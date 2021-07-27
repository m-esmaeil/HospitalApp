using System;
using System.Collections.Generic;

#nullable disable

namespace HospitalApp.Models
{
    public partial class Category
    {
        public Category()
        {
            AccountsTree = new HashSet<AccountsTree>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AccountsTree> AccountsTree { get; set; }
    }
}
