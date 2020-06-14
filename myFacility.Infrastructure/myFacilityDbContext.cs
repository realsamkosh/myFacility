using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class myFacilityDbContext : DbContext
    {
        public myFacilityDbContext()
        {
        }

        public myFacilityDbContext(DbContextOptions<myFacilityDbContext> options)
            : base(options)
        {
        }
    }
}
