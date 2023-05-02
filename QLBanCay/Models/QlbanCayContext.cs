using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLBanCay.Models;

public partial class QlbanCayContext : DbContext
{
    public QlbanCayContext()
    {
    }

    public QlbanCayContext(DbContextOptions<QlbanCayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhChiTiet> AnhChiTiets { get; set; }

    public virtual DbSet<ChiTietCay> ChiTietCays { get; set; }

    public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; }

    public virtual DbSet<DanhMucCay> DanhMucCays { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiCay> LoaiCays { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NuocSanXuat> NuocSanXuats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-454CCL8;Initial Catalog=QLBanCay;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhChiTiet>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet);

            entity.ToTable("AnhChiTiet");

            entity.Property(e => e.MaChiTiet).HasMaxLength(50);

            entity.HasOne(d => d.MaChiTietNavigation).WithOne(p => p.AnhChiTiet)
                .HasForeignKey<AnhChiTiet>(d => d.MaChiTiet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnhChiTiet_ChiTietCay");
        });

        modelBuilder.Entity<ChiTietCay>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet);

            entity.ToTable("ChiTietCay");

            entity.Property(e => e.MaChiTiet).HasMaxLength(50);
            entity.Property(e => e.MaCay).HasMaxLength(50);
            entity.Property(e => e.Slton).HasColumnName("SLTon");

            entity.HasOne(d => d.MaCayNavigation).WithMany(p => p.ChiTietCays)
                .HasForeignKey(d => d.MaCay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietCay_DanhMucCay");
        });

        modelBuilder.Entity<ChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.MaHdb, e.MaChiTiet });

            entity.ToTable("ChiTietHDB");

            entity.Property(e => e.MaHdb)
                .HasMaxLength(50)
                .HasColumnName("MaHDB");
            entity.Property(e => e.MaChiTiet).HasMaxLength(50);
            entity.Property(e => e.GiamGia).HasMaxLength(50);
            entity.Property(e => e.Slban).HasColumnName("SLBan");

            entity.HasOne(d => d.MaChiTietNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaChiTiet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_ChiTietCay");

            entity.HasOne(d => d.MaHdbNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_HoaDonBan");
        });

        modelBuilder.Entity<DanhMucCay>(entity =>
        {
            entity.HasKey(e => e.MaCay);

            entity.ToTable("DanhMucCay");

            entity.Property(e => e.MaCay).HasMaxLength(50);
            entity.Property(e => e.CachChamSoc).HasMaxLength(200);
            entity.Property(e => e.MaLoai).HasMaxLength(50);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNuocSx)
                .HasMaxLength(50)
                .HasColumnName("MaNuocSX");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.TenCay).HasMaxLength(50);

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.DanhMucCays)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhMucCay_NhaCungCap");

            entity.HasOne(d => d.MaNuocSxNavigation).WithMany(p => p.DanhMucCays)
                .HasForeignKey(d => d.MaNuocSx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhMucCay_NuocSanXuat");
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHdb);

            entity.ToTable("HoaDonBan");

            entity.Property(e => e.MaHdb)
                .HasMaxLength(50)
                .HasColumnName("MaHDB");
            entity.Property(e => e.GhiChu)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayBan).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HoaDonBan_KhachHang");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_HoaDonBan_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_KhachHang_User");
        });

        modelBuilder.Entity<LoaiCay>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiCay");

            entity.Property(e => e.MaLoai).HasMaxLength(50);
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.AnhDaiDien).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_NhanVien_User");
        });

        modelBuilder.Entity<NuocSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaNuocSx);

            entity.ToTable("NuocSanXuat");

            entity.Property(e => e.MaNuocSx)
                .HasMaxLength(50)
                .HasColumnName("MaNuocSX");
            entity.Property(e => e.TenNuocSx)
                .HasMaxLength(50)
                .HasColumnName("TenNuocSX");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName);

            entity.ToTable("User");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.LoaiUser).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
