using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;


#nullable disable

namespace TravelExperts.Models
{
    public partial class TravelExpertsContext : IdentityDbContext<User>
    {
        public TravelExpertsContext(DbContextOptions<TravelExpertsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Affiliation> Affiliations { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomersReward> CustomersRewards { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsSupplier> ProductsSuppliers { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierContact> SupplierContacts { get; set; }
        public virtual DbSet<TripType> TripTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Affiliation>(entity =>
            {
                entity.HasKey(e => e.AffilitationId)
                    .HasName("aaaaaAffiliations_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.HasKey(e => e.AgencyId)
                    .HasName("PK_Agencies")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_Agents_Agencies");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("aaaaaBookings_PK")
                    .IsClustered(false);

                entity.Property(e => e.PackageId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Bookings_FK00");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("Bookings_FK01");

                entity.HasOne(d => d.TripType)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TripTypeId)
                    .HasConstraintName("Bookings_FK02");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingDetailId)
                    .HasName("aaaaaBookingDetails_PK")
                    .IsClustered(false);

                entity.Property(e => e.BookingId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductSupplierId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_BookingDetails_Bookings");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_BookingDetails_Classes");

                entity.HasOne(d => d.Fee)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.FeeId)
                    .HasConstraintName("FK_BookingDetails_Fees");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .HasConstraintName("FK_BookingDetails_Products_Suppliers");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_BookingDetails_Regions");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("aaaaaClasses_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CreditCardId)
                    .HasName("aaaaaCreditCards_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditCards_FK00");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("aaaaaCustomers_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Customers_Agents");
            });

            modelBuilder.Entity<CustomersReward>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RewardId })
                    .HasName("aaaaaCustomers_Rewards_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK00");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK01");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.FeeId)
                    .HasName("aaaaaFees_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("aaaaaRegions_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.RewardId)
                    .HasName("aaaaaRewards_PK")
                    .IsClustered(false);

                entity.Property(e => e.RewardId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SupplierContact>(entity =>
            {
                entity.HasKey(e => e.SupplierContactId)
                    .HasName("aaaaaSupplierContacts_PK")
                    .IsClustered(false);

                entity.Property(e => e.SupplierContactId).ValueGeneratedNever();

                entity.Property(e => e.SupplierId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Affiliation)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.AffiliationId)
                    .HasConstraintName("SupplierContacts_FK00");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("SupplierContacts_FK01");
            });

            modelBuilder.Entity<TripType>(entity =>
            {
                entity.HasKey(e => e.TripTypeId)
                    .HasName("aaaaaTripTypes_PK")
                    .IsClustered(false);
            });

            modelBuilder.ApplyConfiguration(new PackageConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new SupplierConfig());
            modelBuilder.ApplyConfiguration(new ProductsSupplierConfig());
            modelBuilder.ApplyConfiguration(new PackagesProductsSupplierConfig());

            OnModelCreatingPartial(modelBuilder);
        }


        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                UserManager<User> userManager = (UserManager<User>)
                    scope.ServiceProvider.GetService(typeof(UserManager<User>));
                RoleManager<IdentityRole> roleManager = (RoleManager<IdentityRole>)
                    scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

                string username = "admin";
                string password = "adminadmin";
                string roleName = "Admin";
                await CreateUser_Role(userManager, roleManager, username, password, roleName);

                username = "customer";
                password = "customer";
                roleName = "Customer";
                await CreateUser_Role(userManager, roleManager, username, password, roleName);

                username = "agent";
                password = "agentagent";
                roleName = "Agent";
                await CreateUser_Role(userManager, roleManager, username, password, roleName);
            }
        }

        private static async Task CreateUser_Role(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, string username, string password, string roleName)
        {
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
