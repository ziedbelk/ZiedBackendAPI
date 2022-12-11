using Microsoft.EntityFrameworkCore;
using ZiedBackendAPI.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ZiedBackendAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute]DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         
            modelBuilder.Entity<PersonSector>()
                .HasKey(x => new { x.SectorId, x.PersonId });

          

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonSector> PersonSectors { get; set; }



    }
}
