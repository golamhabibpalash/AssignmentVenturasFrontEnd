using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AssignmentVenturasFrontEnd.Models;

namespace AssignmentVenturasFrontEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AssignmentVenturasFrontEnd.Models.AddressBook> AddressBook { get; set; }
        public DbSet<AssignmentVenturasFrontEnd.Models.AddressType> AddressType { get; set; }
    }
}
