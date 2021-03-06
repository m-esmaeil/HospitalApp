using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "يجب اختيار التاريخ")]
        public DateTime date { get; set; }


        [Required(ErrorMessage ="يجب اختيار وصف القيد")]
        public string Description { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
