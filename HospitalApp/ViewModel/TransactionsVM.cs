using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.ViewModel
{
    public class TransactionsVM
    {
        public DateTime DateVM { get; set; }
        public int SerialNumberIdVM { get; set; }
        public EntriesSerialize EntrySerialize { get; set; }
        public int AccountTreeIdVM { get; set; }
        public AccountsTree AccountsTreeVM { get; set; }
        public decimal ValueDebit { get; set; }
        public decimal ValueCredit { get; set; }
        public List<AccountsTree> AccountsTreeList { get; set; }

        DataContext db = new DataContext();
        public int serial2Display()
        {
            return db.EntriesSerialize.Select(x => x.Id).Count() + 1;
        }

    }
}
