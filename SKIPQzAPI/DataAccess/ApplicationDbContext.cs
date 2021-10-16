using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKIPQzAPI.Models;
using SKIPQzAPI.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<TimeComponent> TimeComponents { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<TimeComponentInterval> TimeComponentIntervals { get; set; }
        public DbSet<ServiceProviderServices> ServiceProviderServices{get;set;}
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<ServiceExtras> ServiceExtras { get; set; }
        public DbSet<ClientInfo> ClientInfo { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
