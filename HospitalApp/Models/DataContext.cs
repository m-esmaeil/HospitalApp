using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HospitalApp.ViewModel;

#nullable disable

namespace HospitalApp.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountsTree> AccountsTree { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<EntriesSerialize> EntriesSerialize { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-61PK2A3; Database=AccountingDb; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountsTree>(entity =>
            {
                entity.ToTable("AccountsTree");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.AccountsTree)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsTree_Categories");

                /*
                entity.HasOne(d => d.Parent)
                       .WithMany(d => d.Children)
                       .HasForeignKey(d => d.FollowTo)
                       .OnDelete(DeleteBehavior.ClientSetNull);
                */
            });

            //modelBuilder.Entity<AcountType>(entity =>
            //{
            //    entity.ToTable("AcountType");

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(50);
            //});

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<EntriesSerialize>(entity =>
            {
                entity.ToTable("EntriesSerialize");

                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Serial).IsRequired();

                entity.Property(e => e.date).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                //entity.Property(e => e.Date).HasColumnType("datetime");
                //entity.Property(e => e.Date).HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.ValueCredit).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.ValuDebit).HasColumnType("decimal(18,0)");

                entity.HasOne(d => d.AccountsTree)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountTreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_AccountsTree");

                //entity.HasOne(d => d.AccountType)
                //    .WithMany(p => p.Transactions)
                //    .HasForeignKey(d => d.AccountTypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Transactions_AcountType");

                entity.HasOne(d => d.entriesSerialize)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SerialNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_EntriesSerialize");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
