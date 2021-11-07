using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.ViewModel
{
    public class TransactionsVM
    {
        [Required(ErrorMessage ="يحب ادخال التاريخ")]
        public DateTime DateVM { get; set; }

        public int SerialNumberIdVM { get; set; }

        [Required(ErrorMessage = "يجب ادخال وصف القيد")]
        public string Description { get; set; }

        [Required(ErrorMessage = "يجب ادخال اسم الحساب")]
        public int AccountTreeIdVM { get; set; }
        public AccountsTree AccountsTreeVM { get; set; }
        public decimal[] ValueDebit { get; set; }
        public decimal[] ValueCredit { get; set; }
        public List<AccountsTree> AccountsTreeList { get; set; }
    }

    public class TransactionForm
    {
        [Required(ErrorMessage ="من فضلك اخل التاريخ")]
        public DateTime DateVm { get; set; }
        [Required(ErrorMessage ="ادخل وصف القيد")]
        public string Description { get; set; }
        [Required(ErrorMessage ="مطلوب اختيار اسم الحساب")]
        public int[] account { get; set; }
        public decimal?[] debit { get; set; }
        public decimal?[] credit { get; set; }

        public int[] TransIds { get; set; }

        public IEnumerable<Transaction> GetTransactions(int serialNumberId=0)
        {
            var transList = new List<Transaction>();

            for (int i = 0; i < account.Length; i++)
            {
                transList.Add(new Transaction()
                {
                    Id = TransIds[i],
                    AccountTreeId = account[i],
                    ValuDebit = debit[i],
                    ValueCredit = credit[i],
                    SerialNumberId = serialNumberId
                });
            }
            
            return transList.AsEnumerable();
        }
    }
}
