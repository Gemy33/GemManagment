using GemManagment.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.DAL.Data.Context
{
    public class GemDbcontext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Member> Member { get; set; }
        public DbSet<MemberPlan> MemberPlan { get; set; }
        public DbSet<Plans> Plans { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<HealthRecord> HealthRecord { get; set; }
        public DbSet<MemberSession> MemberSession { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Category> Category { get; set; }










        public GemDbcontext(DbContextOptions<GemDbcontext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("server=.;database = GemDb;trusted_connection = true;trustservercertificate=true");
        //    base.OnConfiguring(optionsBuilder);
        //}
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>().Property(p => p.FirstName).HasColumnType("varchar(100)").IsRequired();
        }
    }
}
