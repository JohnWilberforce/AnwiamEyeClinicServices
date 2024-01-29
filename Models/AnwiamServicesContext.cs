using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AnwiamEyeClinicServices.Models;

namespace AnwiamEyeClinicServices.Models;

public partial class AnwiamServicesContext : DbContext
{
    public AnwiamServicesContext()
    {
    }

    public AnwiamServicesContext(DbContextOptions<AnwiamServicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consultation> Consultations { get; set; }

    public virtual DbSet<FrameStock> FrameStocks { get; set; }

    public virtual DbSet<Oct> Octs { get; set; }

    public virtual DbSet<Opd> Opds { get; set; }
    public virtual DbSet<RevenueServices> RevenueServicies { get; set; }
    public virtual DbSet<OPDConsultStatus> OPDConsultStatuses { get; set; }
    public virtual DbSet<ImageReport> ImageReports { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<RefractionPx> RefractionPxes { get; set; }
    public virtual DbSet<VFTreport> VFTreports { get; set; } = null!;
    public virtual DbSet<OCTreport> OCTreports { get; set; } = null!;
    public virtual DbSet<Total> Totals { get; set; }
    public virtual DbSet<PurchaseRefractionPx> PurchaseRefractionPxes { get; set; } = null!;
    public virtual DbSet<Pharmacy> Pharmacys { get; set; }
    public virtual DbSet<RetinalImage> RetinalImages { get; set; }


    public virtual DbSet<Vft> Vfts { get; set; }
    //
    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M1NK2O8\\SQLEXPRESS;Database=AnwiamServices;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pharmacy>(entity =>
        {

            entity.ToTable("Pharmacy");
        });
        modelBuilder.Entity<RetinalImage>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RetinalImage");
        });
            modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consulta__3214EC07BC358BDE");

            entity.ToTable("Consultation");

            entity.Property(e => e.ChiefComplaint)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FamilyHistory)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Medications)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PatientHistory)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PatientId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SpectacleRx)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Conjunctiva).HasMaxLength(200);
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.Eyelids).HasMaxLength(200);
            entity.Property(e => e.Cornea).HasMaxLength(200);
            entity.Property(e => e.Pupils).HasMaxLength(200);
            entity.Property(e => e.Media).HasMaxLength(200);
            entity.Property(e => e.Lens).HasMaxLength(200);
            entity.Property(e => e.OpticNerve).HasMaxLength(200);
            entity.Property(e => e.Fundus).HasMaxLength(200);
            entity.Property(e => e.VisualAcuity)
                .HasMaxLength(200)
                .IsUnicode(false);
        });
        modelBuilder.Entity<PurchaseRefractionPx>(entity =>
        {
            entity.HasNoKey();
        });
            modelBuilder.Entity<FrameStock>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FrameStock");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FrameType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Oct>(entity =>
        {
            entity.HasKey(e => e.ScanId);
                entity.ToTable("OCT");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Macula)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Onh)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ONH");
            entity.Property(e => e.Pachymetry)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pachymetry");
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patientName");
            entity.Property(e => e.ReFfacility)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("reFfacility");
            entity.Property(e => e.ReferredDrName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referredDrName");
            entity.Property(e => e.ScanId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("scanId");
        });

        modelBuilder.Entity<Opd>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OPD__3214EC07BC94CF90");

            entity.ToTable("OPD");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Contact).HasMaxLength(15);
            entity.Property(e=>e.Status).HasMaxLength(15);
            entity.Property(e => e.Date)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PatientId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Services)
                .HasMaxLength(120)
                .IsUnicode(false);
        });
        modelBuilder.Entity<RevenueServices>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("RevenueServices");


            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Date)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Services)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Status)
    .HasMaxLength(15)
    .IsUnicode(false);
        });

        modelBuilder.Entity<OPDConsultStatus>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("OPDConsultStatus");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Contact).HasMaxLength(15);
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.Date)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PatientId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Services)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId);
                entity.ToTable("Purchase");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PatientName)
            .IsRequired();
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.FramePrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FrameType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LensPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LensType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseId).ValueGeneratedOnAdd();
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<RefractionPx>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RefractionPx");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SpectacleRx)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vft>(entity =>
        {
            entity.HasKey(e => e.ScanId);

            entity.ToTable("VFT");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patientName");
            entity.Property(e => e.ReFfacility)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("reFfacility");
            entity.Property(e => e.ReferredDrName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referredDrName");
            entity.Property(e => e.ScanId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("scanId");
        });
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
