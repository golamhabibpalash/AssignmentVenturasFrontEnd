using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class AddressBook
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Address { get; set; }

        [Display(Name ="Type")]
        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }
        public string Remarks { get; set; }

        //public ICollection<AddressBook> AddressBooks { get; set; }
    }
}
