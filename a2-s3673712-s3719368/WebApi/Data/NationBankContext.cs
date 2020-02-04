using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class NationBankContext : DbContext
    {
        public NationBankContext()
        {
        }

        public NationBankContext(DbContextOptions<NationBankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<BillPays> BillPays { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Payees> Payees { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=wdt2020.australiasoutheast.cloudapp.azure.com;Database=s3673712;uid=s3673712;pwd=abc123;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccountNumber);

                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.AccountNumber).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ModifyDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<BillPays>(entity =>
            {
                entity.HasKey(e => e.BillPayId);

                entity.HasIndex(e => e.AccountNumber);

                entity.HasIndex(e => e.PayeeId);

                entity.Property(e => e.BillPayId).HasColumnName("BillPayID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.PayeeId).HasColumnName("PayeeID");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.BillPays)
                    .HasForeignKey(d => d.AccountNumber);

                entity.HasOne(d => d.Payee)
                    .WithMany(p => p.BillPays)
                    .HasForeignKey(d => d.PayeeId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.PostCode).HasMaxLength(4);

                entity.Property(e => e.State).HasMaxLength(3);

                entity.Property(e => e.Tfn)
                    .HasColumnName("TFN")
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<Logins>(entity =>
            {
                entity.HasKey(e => e.LoginId);

                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .HasMaxLength(8);

                entity.Property(e => e.Attempt).HasColumnName("attempt");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Lock)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.LockDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.ModifyDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<Payees>(entity =>
            {
                entity.HasKey(e => e.PayeeId);

                entity.Property(e => e.PayeeId).HasColumnName("PayeeID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.PayeeName).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.PostCode).HasMaxLength(4);

                entity.Property(e => e.State).HasMaxLength(3);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.HasIndex(e => e.AccountNumber);

                entity.HasIndex(e => e.DestinationAccountNumber);

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.TransactionsAccountNumberNavigation)
                    .HasForeignKey(d => d.AccountNumber);

                entity.HasOne(d => d.DestinationAccountNumberNavigation)
                    .WithMany(p => p.TransactionsDestinationAccountNumberNavigation)
                    .HasForeignKey(d => d.DestinationAccountNumber);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
