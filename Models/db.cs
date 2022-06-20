using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    public class db : DbContext
    {
        public DbSet<Signup> Signups { get; set; }
    }
}