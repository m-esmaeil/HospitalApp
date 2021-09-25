using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.ViewModel
{
    public class AccountsSumVM
    {
        public string Account { get; set; }
        public decimal? SumDebit { get; set; }
        public decimal? SumCredit { get; set; }
    }
}
