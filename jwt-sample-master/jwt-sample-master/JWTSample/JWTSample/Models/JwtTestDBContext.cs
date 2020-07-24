using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace JWTSample.Models
{
    public partial class JwtTestDBContext : DbContext
    {
        public JwtTestDBContext()
        {
        }

        public JwtTestDBContext(DbContextOptions<JwtTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AçıkKapıSosyalHizmetBaş1> AçıkKapıSosyalHizmetBaş1 { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Foundation> Foundation { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        /*
         
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
         */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AçıkKapıSosyalHizmetBaş1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("'Açık Kapı Sosyal Hizmet Baş_1$'");

                entity.Property(e => e.Adı).HasMaxLength(255);

                entity.Property(e => e.BasvuruKonu).HasMaxLength(255);

                entity.Property(e => e.BasvuruTarihi).HasColumnType("datetime");

                entity.Property(e => e.CepTelNo)
                    .HasColumnName("Cep Tel#No")
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(255);

                entity.Property(e => e.Soyadı).HasMaxLength(255);

                entity.Property(e => e.TcKimlikNo)
                    .HasColumnName("TC Kimlik No")
                    .HasMaxLength(255);

                entity.Property(e => e.İl).HasMaxLength(255);

                entity.Property(e => e.İlçe).HasMaxLength(255);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.County)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_il_no");
            });

            modelBuilder.Entity<Foundation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountyId).HasColumnName("CountyID");

                entity.Property(e => e.Email)
                    .HasColumnName("EMail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Foundation)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Foundation_County");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Foundation)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Foundation_Province");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.ProvinceId)
                    .HasColumnName("ProvinceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
