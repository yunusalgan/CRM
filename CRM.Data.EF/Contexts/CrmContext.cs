using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CRM.Data.EF.Contexts
{
    public partial class CrmContext : DbContext
    {
        private IConfiguration _config;

        public CrmContext()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _config = configBuilder.Build();
        }

        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {

        }


        public virtual DbSet<Entities.Netah.SampleEntity> SampleEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_config["Configuration:CONNECTION_STRING_DEBUG"]);
#else
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_config["Configuration:CONNECTION_STRING_RELEASE"]);
#endif

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Netah.SampleEntity>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

            });
        }

    }
}
