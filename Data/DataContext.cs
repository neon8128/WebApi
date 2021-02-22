using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Server_Try02.fake;
using Server_Try02.Models;
using WebApi.Models;

//#nullable disable

namespace WebApi.Data
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

        
        public virtual DbSet<AuditModel> DbAudit { get; set; }
        public virtual DbSet<UserModel> DbUsers { get; set; }
        public virtual DbSet<UserHistModel> DbUserHists { get; set; }

        public virtual DbSet<LocationModel> DbLocations { get; set;} 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySql("*", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditModel>(entity =>
            {
                entity.ToTable("audit");

                entity.Property(e => e.Id).HasColumnName("audit_id").ValueGeneratedOnAdd();

                entity.Property(e => e.Details)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("details")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Ip)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("ip")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MachineName)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("machine_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Ts)
                    .HasColumnType("datetime")
                    .HasColumnName("TS");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => new { e.Username})
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0});

                entity.ToTable("user");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("VARCHAR(160)")
                    .HasColumnName("passwordHash")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'user'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnType("VARCHAR(160)")
                    .HasColumnName("salt")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("'1'");
            });

             modelBuilder.Entity<LocationModel>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.AuditId, "audit_id_idx");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");
                /*
                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("audit_id"); */
            });

            modelBuilder.Entity<UserHistModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                 entity.ToTable("user_hist");

                entity.HasIndex(e => e.Username, "username_idx");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("varchar(160)")
                    .HasColumnName("passwordHash")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Salt)
                    .HasColumnType("varchar(160)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'user'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                    entity.Property(e => e.Email)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("'1'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
