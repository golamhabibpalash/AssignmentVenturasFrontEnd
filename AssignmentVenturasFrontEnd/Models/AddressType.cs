using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentVenturasFrontEnd.Models
{
    public class AddressType
    {
        public int Id { get; set; }
        public string AddressTypeName { get; set; }
        public ICollection<AddressBook> AddressBooks { get; set; }
    }
}
