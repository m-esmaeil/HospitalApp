using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.ViewModel
{
    public class AccountsTreeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FollowTo { get; set; }
        public string FollowName { get; set; }
        public int CategoryId { get; set; }
        public string CatName { get; set; }
        public Category Categories { get; set; }
        public virtual IList<Category> CategoriesVM { get; set; }
        public virtual AccountsTree accountstree{ get; set; }
        public virtual IList<AccountsTree> accountsTreeVM { get; set; }
    }
}
