using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<ServiceProviderServices> ServiceProviderServices{get;set;}
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
