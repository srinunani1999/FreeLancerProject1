using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FreeLancerProject1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FreeLancerProject1.Data
{
    public class FreeLancerProject1Context : DbContext
    {
        public FreeLancerProject1Context (DbContextOptions<FreeLancerProject1Context> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
       public DbSet<EmployeeViewModel> Employee2 { get; set; }

        public  DbSetFindingConvention MyProperty { get; set; }



    }
}
