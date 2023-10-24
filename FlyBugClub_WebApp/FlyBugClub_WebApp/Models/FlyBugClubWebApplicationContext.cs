using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlyBugClub_WebApp.Models;

public partial class FlyBugClubWebApplicationContext : DbContext
{
    public FlyBugClubWebApplicationContext()
    {
    }

    public FlyBugClubWebApplicationContext(DbContextOptions<FlyBugClubWebApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BillBorrow> BillBorrows { get; set; }

    public virtual DbSet<BorrowDetail> BorrowDetails { get; set; }

    public virtual DbSet<BorrowRate> BorrowRates { get; set; }

    public virtual DbSet<CategoryDevice> CategoryDevices { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceSheetPrice> DeviceSheetPrices { get; set; }

    public virtual DbSet<DiscountDevice> DiscountDevices { get; set; }

    public virtual DbSet<HistoryUpdate> HistoryUpdates { get; set; }

    public virtual DbSet<HoSoTuyenDung> HoSoTuyenDungs { get; set; }

    public virtual DbSet<OrderDevice> OrderDevices { get; set; }

    public virtual DbSet<OrderDeviceDetail> OrderDeviceDetails { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<YeuCauUngTuyen> YeuCauUngTuyens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;uid=ngunemay123_web_clb;password=conchongu0123;database=ngunemay123_web_clb;Encrypt=true;TrustServerCertificate=true");

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //    => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;uid=sa;password=1;database=FlyBugClub_WebApplication;Encrypt=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Faculty).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(5);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.PositionId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PositionID");
            entity.Property(e => e.Uid)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("UID");
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

        modelBuilder.Entity<BillBorrow>(entity =>
        {
            entity.HasKey(e => e.Bid);

            entity.ToTable("BillBorrow");

            entity.Property(e => e.Bid).HasColumnName("BID");
            entity.Property(e => e.BorrowDate).HasColumnType("datetime");
            entity.Property(e => e.DepositPriceOnBill).HasColumnType("money");
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.ReceiveDay).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.Sid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.BillBorrows)
                .HasForeignKey(d => d.Sid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillBorrow_User1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.BillBorrows)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillBorrow_Supplier");
        });

        modelBuilder.Entity<BorrowDetail>(entity =>
        {
            entity.ToTable("BorrowDetail");

            entity.Property(e => e.BorrowDetailId).HasColumnName("BorrowDetailID");
            entity.Property(e => e.Bid).HasColumnName("BID");
            entity.Property(e => e.DepositPrice).HasColumnType("money");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SubTotal).HasColumnType("money");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.BorrowDetails)
                .HasForeignKey(d => d.Bid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowDetail_BillBorrow");

            entity.HasOne(d => d.Device).WithMany(p => p.BorrowDetails)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowDetail_Device");
        });

        modelBuilder.Entity<BorrowRate>(entity =>
        {
            entity.HasKey(e => new { e.DeviceId, e.Uid });

            entity.ToTable("BorrowRate");

            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.Uid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Device).WithMany(p => p.BorrowRates)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowRate_Device");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.BorrowRates)
                .HasForeignKey(d => d.Uid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BorrowRate_User");
        });

        modelBuilder.Entity<CategoryDevice>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Category");

            entity.ToTable("CategoryDevice");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("Device");

            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Describe).HasMaxLength(200);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Devices)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_CategoryDevice");
        });

        modelBuilder.Entity<DeviceSheetPrice>(entity =>
        {
            entity.HasKey(e => e.SheetPriceId);

            entity.ToTable("Device_SheetPrice");

            entity.Property(e => e.SheetPriceId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SheetPriceID");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Device).WithMany(p => p.DeviceSheetPrices)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_SheetPrice_Device");
        });

        modelBuilder.Entity<DiscountDevice>(entity =>
        {
            entity.ToTable("Discount_Device");

            entity.Property(e => e.DiscountDeviceId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("DiscountDeviceID");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Device).WithMany(p => p.DiscountDevices)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discount_Device_Device");
        });

        modelBuilder.Entity<HistoryUpdate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HistoryUpdate");

            entity.Property(e => e.Bid).HasColumnName("BID");
            entity.Property(e => e.BorrowDetailId).HasColumnName("BorrowDetailID");
            entity.Property(e => e.Uid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BorrowDetail).WithMany()
                .HasForeignKey(d => d.BorrowDetailId)
                .HasConstraintName("FK_HistoryUpdate_BillBorrow");

            entity.HasOne(d => d.BorrowDetailNavigation).WithMany()
                .HasForeignKey(d => d.BorrowDetailId)
                .HasConstraintName("FK_HistoryUpdate_BorrowDetail");

            entity.HasOne(d => d.UidNavigation).WithMany()
                .HasForeignKey(d => d.Uid)
                .HasConstraintName("FK_HistoryUpdate_User");
        });

        modelBuilder.Entity<HoSoTuyenDung>(entity =>
        {
            entity.HasKey(e => e.MaViTri);

            entity.ToTable("HoSoTuyenDung");

            entity.Property(e => e.TenViTri).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDevice>(entity =>
        {
            entity.HasKey(e => e.Oid);

            entity.ToTable("OrderDevice");

            entity.Property(e => e.Oid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("OID");
            entity.Property(e => e.DateOrder).HasColumnType("date");
            entity.Property(e => e.SupplierAddress).HasMaxLength(100);
            entity.Property(e => e.SupplierName).HasMaxLength(50);
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(11)
                .IsFixedLength();
        });

        modelBuilder.Entity<OrderDeviceDetail>(entity =>
        {
            entity.HasKey(e => e.Odid);

            entity.ToTable("OrderDeviceDetail");

            entity.Property(e => e.Odid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ODID");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.Oid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("OID");

            entity.HasOne(d => d.Device).WithMany(p => p.OrderDeviceDetails)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDeviceDetail_Device");

            entity.HasOne(d => d.OidNavigation).WithMany(p => p.OrderDeviceDetails)
                .HasForeignKey(d => d.Oid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDeviceDetail_OrderDevice");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.PositionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PositionID");
            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK_BillInformation");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Room)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SupplierAddress).HasMaxLength(100);
            entity.Property(e => e.SupplierName).HasMaxLength(20);
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(11)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("User");

            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Faculty).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PositionId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PositionID");
        });

        modelBuilder.Entity<YeuCauUngTuyen>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK_Member");

            entity.ToTable("YeuCauUngTuyen");

            entity.Property(e => e.StudentId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.AdvantagesNote).HasMaxLength(100);
            entity.Property(e => e.DisadvantagesNote).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Faculty).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(5);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsFixedLength();
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.ReasonNote).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
