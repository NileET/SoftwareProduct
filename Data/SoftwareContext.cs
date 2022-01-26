using Microsoft.EntityFrameworkCore;
using Software.Models;

namespace Software.Data
{
    public partial class SoftwareContext : DbContext
    {
        public SoftwareContext(DbContextOptions<SoftwareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DevelopmentPlayer> DevelopmentPlayers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<SoftwareProduct> SoftwareProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contracts");

                entity.HasIndex(e => e.CustomerId, "customer_id");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.ContractDate).HasColumnName("contract_date");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contracts_ibfk_1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(70)
                    .HasColumnName("address");

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(35)
                    .HasColumnName("contact_person");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("customer_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<DevelopmentPlayer>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PRIMARY");

                entity.ToTable("development_players");

                entity.HasIndex(e => e.EmployeeId, "employee_id");

                entity.HasIndex(e => e.ProductId, "product_id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeRole)
                    .HasMaxLength(20)
                    .HasColumnName("employee_role");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DevelopmentPlayers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("development_players_ibfk_1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DevelopmentPlayers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("development_players_ibfk_2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("employee_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate).HasColumnName("hire_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .HasColumnName("middle_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PRIMARY");

                entity.ToTable("product_type");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.TypeId)
                    .HasMaxLength(10)
                    .HasColumnName("type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SoftwareProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");

                entity.ToTable("software_products");

                entity.HasIndex(e => e.ContractId, "contract_id")
                    .IsUnique();

                entity.HasIndex(e => e.ProductName, "name")
                    .IsUnique();

                entity.HasIndex(e => e.TypeId, "type_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ContractId).HasColumnName("contract_id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(30)
                    .HasColumnName("product_name");

                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

                entity.Property(e => e.TypeId)
                    .HasMaxLength(10)
                    .HasColumnName("type_id");

                entity.Property(e => e.Version)
                    .HasMaxLength(20)
                    .HasColumnName("version");

                entity.HasOne(d => d.Contract)
                    .WithOne(p => p.SoftwareProduct)
                    .HasForeignKey<SoftwareProduct>(d => d.ContractId)
                    .HasConstraintName("software_products_ibfk_2");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SoftwareProducts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("software_products_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
