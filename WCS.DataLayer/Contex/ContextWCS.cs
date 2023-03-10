

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WCS.ClassDomain.Domains;
using WCS.FluentAPI.Configures;

namespace WCS.DataLayer.Contex
{
    public class ContextWCS : DbContext
    {
        public DbSet<Business> Business { get; set; }
        public DbSet<CivilContract> CivilContract { get; set; }
        public DbSet<Finance> Finance { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<WornCars> WornCars { get; set; }
        public DbSet<WornCenter> WornCenter { get; set; }
        public DbSet<WornMaster> WornMaster { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Change Table Name
            modelBuilder.Entity<Business>().ToTable("WCS_Business");
            modelBuilder.Entity<CivilContract>().ToTable("WCS_CivilContract");
            modelBuilder.Entity<Finance>().ToTable("WCS_Finance");
            modelBuilder.Entity<Owners>().ToTable("WCS_Owners");
            modelBuilder.Entity<WornCars>().ToTable("WCS_WornCars");
            modelBuilder.Entity<WornCenter>().ToTable("WCS_WornCenter");
            modelBuilder.Entity<WornMaster>().ToTable("WCS_WornMaster");
            modelBuilder.Entity<Product>().ToTable("WCS_Product");
            modelBuilder.Entity<Order>().ToTable("WCS_Order");
            modelBuilder.Entity<Message>().ToTable("WCS_Message");
            modelBuilder.Entity<ProductGroup>().ToTable("WCS_ProductGroup");
            modelBuilder.Entity<ProductPrice>().ToTable("WCS_ProductPrice");
            modelBuilder.Entity<OrderTransaction>().ToTable("WCS_OrderTransaction");

            // Language
            //  modelBuilder.Entity<Language>().Property(L => L.Language_Rtl).HasDefaultValue(true);



            //Set  Configuration
            modelBuilder.ApplyConfiguration(new ConfigureBusiness());
            modelBuilder.ApplyConfiguration(new ConfigureCivilContract());
            modelBuilder.ApplyConfiguration(new ConfigureFinance());
            modelBuilder.ApplyConfiguration(new ConfigureOwners());
            modelBuilder.ApplyConfiguration(new ConfigureWornCars());
            modelBuilder.ApplyConfiguration(new ConfigureWornCenter());
            modelBuilder.ApplyConfiguration(new ConfigureWornMaster());
            modelBuilder.ApplyConfiguration(new ConfigureProduct());
            modelBuilder.ApplyConfiguration(new ConfigureOrder());
            modelBuilder.ApplyConfiguration(new ConfigureMessage());
            modelBuilder.ApplyConfiguration(new ConfigureProductGroup());
            modelBuilder.ApplyConfiguration(new ConfigureProductPrice());
            modelBuilder.ApplyConfiguration(new ConfigureOrderTransaction());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

        public ContextWCS(DbContextOptions<ContextWCS> options)
      : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var SqlConnectionString = Configuration.GetConnectionString("AppDbWCS");
            optionsBuilder.UseSqlServer(SqlConnectionString);

        }

    }
}
