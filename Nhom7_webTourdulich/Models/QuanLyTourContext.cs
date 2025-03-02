using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Models;

public partial class QuanLyTourContext : DbContext
{
    public QuanLyTourContext()
    {
    }

    public QuanLyTourContext(DbContextOptions<QuanLyTourContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhTour> AnhTours { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChiTietNhomTour> ChiTietNhomTours { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DiemDen> DiemDens { get; set; }

    public virtual DbSet<GiaTour> GiaTours { get; set; }

    public virtual DbSet<HanhKhach> HanhKhaches { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhachSan> KhachSans { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiHanhKhach> LoaiHanhKhaches { get; set; }

    public virtual DbSet<LoaiTour> LoaiTours { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NhomTour> NhomTours { get; set; }

    public virtual DbSet<PhuongTien> PhuongTiens { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-IGLC6V19;Database=QuanLyTour;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhTour>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__AnhTour__06C6A463D59595B2");

            entity.ToTable("AnhTour");

            entity.Property(e => e.MaAnh).HasColumnName("ma_anh");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hinh_anh");
            entity.Property(e => e.LoaiFile)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("loai_file");
            entity.Property(e => e.MaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_tour");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.AnhTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnhTour__ma_tour__5535A963");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => new { e.MaHoaDon, e.MaKhachHang }).HasName("PK__ChiTietH__A77ACE4C7973237A");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_hoa_don");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.GiaTour)
                .HasColumnType("money")
                .HasColumnName("gia_tour");
            entity.Property(e => e.SoLuong)
                .HasDefaultValue(1)
                .HasColumnName("so_luong");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([gia_tour]*[so_luong])", true)
                .HasColumnType("money")
                .HasColumnName("thanh_tien");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__ma_ho__6A30C649");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__ma_kh__6B24EA82");
        });

        modelBuilder.Entity<ChiTietNhomTour>(entity =>
        {
            entity.HasKey(e => new { e.MaNhomTour, e.MaNhanVien, e.MaPhuongTien }).HasName("PK__ChiTietN__D6FAF5ACA45EEC89");

            entity.ToTable("ChiTietNhomTour");

            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhom_tour");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhan_vien");
            entity.Property(e => e.MaPhuongTien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_phuong_tien");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.ChiTietNhomTours)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietNh__ma_nh__5CD6CB2B");

            entity.HasOne(d => d.MaNhomTourNavigation).WithMany(p => p.ChiTietNhomTours)
                .HasForeignKey(d => d.MaNhomTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietNh__ma_nh__5BE2A6F2");

            entity.HasOne(d => d.MaPhuongTienNavigation).WithMany(p => p.ChiTietNhomTours)
                .HasForeignKey(d => d.MaPhuongTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietNh__ma_ph__5DCAEF64");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__41374AC9CEDA77E2");

            entity.ToTable("ChucVu");

            entity.Property(e => e.MaChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_chuc_vu");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .HasColumnName("ten_chuc_vu");
        });

        modelBuilder.Entity<DiemDen>(entity =>
        {
            entity.HasKey(e => e.MaDiemDen).HasName("PK__DiemDen__807E1A34F688D729");

            entity.ToTable("DiemDen");

            entity.Property(e => e.MaDiemDen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_diem_den");
            entity.Property(e => e.Ten)
                .HasMaxLength(50)
                .HasColumnName("ten");
            entity.Property(e => e.ThanhPho)
                .HasMaxLength(255)
                .HasColumnName("thanh_pho");
        });

        modelBuilder.Entity<GiaTour>(entity =>
        {
            entity.HasKey(e => e.MaGiaTour).HasName("PK__GiaTour__D361D61CEC5B2EA3");

            entity.ToTable("GiaTour");

            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_gia_tour");
            entity.Property(e => e.Gia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gia");
            entity.Property(e => e.NgayBatDau)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.NgayKetThuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ngay_ket_thuc");
        });

        modelBuilder.Entity<HanhKhach>(entity =>
        {
            entity.HasKey(e => new { e.MaKhachHang, e.MaNhomTour }).HasName("PK__HanhKhac__C7AB99E14DECD744");

            entity.ToTable("HanhKhach");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhom_tour");
            entity.Property(e => e.MaLoaiKhach)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_loai_khach");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HanhKhach__ma_kh__60A75C0F");

            entity.HasOne(d => d.MaLoaiKhachNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.MaLoaiKhach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HanhKhach__ma_lo__619B8048");

            entity.HasOne(d => d.MaNhomTourNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.MaNhomTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HanhKhach__ma_nh__628FA481");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__DBE2D9E37AFC20BC");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_hoa_don");
            entity.Property(e => e.DaThanhToan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("da_thanh_toan");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhom_tour");
            entity.Property(e => e.NgayLap)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ngay_lap");
            entity.Property(e => e.TongTien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tong_tien");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__ma_khach__656C112C");

            entity.HasOne(d => d.MaNhomTourNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNhomTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__ma_nhom___66603565");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__C9817AF6C994CF68");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("dia_chi");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(20)
                .HasColumnName("gioi_tinh");
            entity.Property(e => e.QuocTich)
                .HasMaxLength(50)
                .HasColumnName("quoc_tich");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sdt");
            entity.Property(e => e.Ten)
                .HasMaxLength(50)
                .HasColumnName("ten");
        });

        modelBuilder.Entity<KhachSan>(entity =>
        {
            entity.HasKey(e => e.MaKhachSan).HasName("PK__KhachSan__D39D7708D6B71854");

            entity.ToTable("KhachSan");

            entity.Property(e => e.MaKhachSan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khach_san");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("dia_chi");
            entity.Property(e => e.MaDiemDen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_diem_den");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("so_dien_thoai");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(255)
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDiemDenNavigation).WithMany(p => p.KhachSans)
                .HasForeignKey(d => d.MaDiemDen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachSan__ma_die__4CA06362");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__01A88CB3E8DFB45C");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKhuyenMai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_khuyen_mai");
            entity.Property(e => e.GiaGiam)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gia_giam");
            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_gia_tour");
            entity.Property(e => e.NgayBatDau)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ngay_bat_dau");
            entity.Property(e => e.NgayKetThuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ngay_ket_thuc");
            entity.Property(e => e.PhanTramGiam)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phan_tram_giam");
            entity.Property(e => e.TenKhuyenMai)
                .HasMaxLength(100)
                .HasColumnName("ten_khuyen_mai");

            entity.HasOne(d => d.MaGiaTourNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaGiaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhuyenMai__ma_gi__3D5E1FD2");
        });

        modelBuilder.Entity<LoaiHanhKhach>(entity =>
        {
            entity.HasKey(e => e.MaLoaiKhach).HasName("PK__LoaiHanh__C9C9912943645711");

            entity.ToTable("LoaiHanhKhach");

            entity.Property(e => e.MaLoaiKhach)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_loai_khach");
            entity.Property(e => e.TenLoaiKhach)
                .HasMaxLength(50)
                .HasColumnName("ten_loai_khach");
        });

        modelBuilder.Entity<LoaiTour>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTour).HasName("PK__LoaiTour__CADBC901E10025E3");

            entity.ToTable("LoaiTour");

            entity.Property(e => e.MaLoaiTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_loai_tour");
            entity.Property(e => e.TenLoaiTour)
                .HasMaxLength(50)
                .HasColumnName("ten_loai_tour");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__6781B7B9A42FAD2C");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhan_vien");
            entity.Property(e => e.MaVaiTro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_vai_tro");
            entity.Property(e => e.Ten)
                .HasMaxLength(50)
                .HasColumnName("ten");

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaVaiTro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__ma_vai__47DBAE45");
        });

        modelBuilder.Entity<NhomTour>(entity =>
        {
            entity.HasKey(e => e.MaNhomTour).HasName("PK__NhomTour__E2AE317102AC642B");

            entity.ToTable("NhomTour");

            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_nhom_tour");
            entity.Property(e => e.DiemXuatPhat)
                .HasMaxLength(255)
                .HasColumnName("diem_xuat_phat");
            entity.Property(e => e.MaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_tour");
            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ma_trang_thai");
            entity.Property(e => e.NgayKetThuc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ngay_ket_thuc");
            entity.Property(e => e.NgayKhoiHanh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ngay_khoi_hanh");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.NhomTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhomTour__ma_tou__5812160E");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.NhomTours)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhomTour__ma_tra__59063A47");
        });

        modelBuilder.Entity<PhuongTien>(entity =>
        {
            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__2CDFA6B304C82800");

            entity.ToTable("PhuongTien");

            entity.Property(e => e.MaPhuongTien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_phuong_tien");
            entity.Property(e => e.Ten)
                .HasMaxLength(50)
                .HasColumnName("ten");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("trang_thai");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__1CCF87589BE9F1CC");

            entity.ToTable("Tour");

            entity.Property(e => e.MaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_tour");
            entity.Property(e => e.MaDiemDen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_diem_den");
            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_gia_tour");
            entity.Property(e => e.MaLoaiTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ma_loai_tour");
            entity.Property(e => e.Ten)
                .HasMaxLength(50)
                .HasColumnName("ten");

            entity.HasOne(d => d.MaDiemDenNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaDiemDen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__ma_diem_de__5165187F");

            entity.HasOne(d => d.MaGiaTourNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaGiaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__ma_gia_tou__5070F446");

            entity.HasOne(d => d.MaLoaiTourNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaLoaiTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__ma_loai_to__4F7CD00D");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__5C69DAA82E6D05EA");

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ma_trang_thai");
            entity.Property(e => e.TenTrangThai)
                .HasMaxLength(20)
                .HasColumnName("ten_trang_thai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
