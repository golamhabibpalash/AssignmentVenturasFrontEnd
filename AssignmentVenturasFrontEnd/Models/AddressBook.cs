using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentVenturasFrontEnd.Models
{
    public class AddressBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Remarks { get; set; }
    }
}
