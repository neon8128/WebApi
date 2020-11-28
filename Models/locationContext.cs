using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Server_Try02.Models
{
    public partial class locationContext : DbContext
    {
        public locationContext()
        {
        }

        public locationContext(DbContextOptions<locationContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserHist> UserHists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySql("server=localhost;database=location;uid=admin;pwd=admin", Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.4.13-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.HasIndex(e => e.Username, "user_audit");

                entity.Property(e => e.AuditId)
                    .HasColumnType("int(11)")
                    .HasColumnName("audit_id");

                entity.Property(e => e.Details)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("details")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Ip)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("ip")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longidute).HasColumnName("longidute");

                entity.Property(e => e.MachineName)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("machine_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Ts)
                    .HasColumnType("datetime")
                    .HasColumnName("TS");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Email })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'user'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<UserHist>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_hist");

                entity.HasIndex(e => e.Username, "username");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version")
                    .HasDefaultValueSql("'1'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
