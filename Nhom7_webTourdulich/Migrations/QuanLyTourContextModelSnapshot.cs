﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nhom7_webTourdulich.Models;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    [DbContext(typeof(QuanLyTourContext))]
    partial class QuanLyTourContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DanhGia", b =>
                {
                    b.Property<int>("MaDanhGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDanhGia"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaKhachHang")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDanhGia")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDanhGia");

                    b.HasIndex("MaKhachHang");

                    b.ToTable("DanhGias");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.ChiTietHoaDon", b =>
                {
                    b.Property<string>("MaHoaDon")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Ma_Hoa_Don");

                    b.Property<int>("MaKhachHang")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Khach_Hang");

                    b.Property<decimal>("GiaTour")
                        .HasColumnType("money")
                        .HasColumnName("Gia_Tour");

                    b.Property<int>("SoLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("So_Luong");

                    b.Property<decimal?>("ThanhTien")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("money")
                        .HasColumnName("Thanh_Tien")
                        .HasComputedColumnSql("([Gia_Tour]*[So_Luong])", true);

                    b.HasKey("MaHoaDon", "MaKhachHang")
                        .HasName("PK__ChiTietH__A294B5FB13EB6400");

                    b.HasIndex("MaKhachHang");

                    b.ToTable("ChiTietHoaDon", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.ChucVu", b =>
                {
                    b.Property<int>("MaChucVu")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Chuc_Vu");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaChucVu"));

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ten_Chuc_Vu");

                    b.HasKey("MaChucVu")
                        .HasName("PK__ChucVu__EFE449723DD79615");

                    b.ToTable("ChucVu", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.DiemDen", b =>
                {
                    b.Property<int>("MaDiemDen")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Diem_Den");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDiemDen"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ThanhPho")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Thanh_Pho");

                    b.HasKey("MaDiemDen")
                        .HasName("PK__DiemDen__34267B5E86B79B2E");

                    b.ToTable("DiemDen", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.GiaTour", b =>
                {
                    b.Property<int>("MaGiaTour")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Gia_Tour");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaGiaTour"));

                    b.Property<decimal>("Gia")
                        .HasColumnType("money");

                    b.Property<DateOnly>("NgayBatDau")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Bat_Dau");

                    b.Property<DateOnly>("NgayKetThuc")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Ket_Thuc");

                    b.HasKey("MaGiaTour")
                        .HasName("PK__GiaTour__6B20AB467AE194FF");

                    b.ToTable("GiaTour", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.HoaDon", b =>
                {
                    b.Property<string>("MaHoaDon")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Ma_Hoa_Don");

                    b.Property<string>("DiemDon")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Diem_Don");

                    b.Property<TimeOnly>("GioDi")
                        .HasColumnType("time")
                        .HasColumnName("Gio_Di");

                    b.Property<int>("MaKhachHang")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Khach_Hang");

                    b.Property<int>("MaKhachHangNavigationMaKhachHang")
                        .HasColumnType("int");

                    b.Property<int>("MaNhomTour")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Nhom_Tour");

                    b.Property<DateOnly>("NgayDi")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Di");

                    b.Property<DateOnly>("NgayLap")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Lap");

                    b.Property<string>("ThanhToan")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Thanh_Toan");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("money")
                        .HasColumnName("Tong_Tien");

                    b.HasKey("MaHoaDon")
                        .HasName("PK__HoaDon__91EF063FFF61E16D");

                    b.HasIndex("MaKhachHangNavigationMaKhachHang");

                    b.HasIndex("MaNhomTour");

                    b.ToTable("HoaDon", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.KhachHang", b =>
                {
                    b.Property<int>("MaKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Khach_Hang");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhachHang"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Dia_Chi");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Gioi_Tinh");

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Hinh_Anh");

                    b.Property<int?>("MaTour")
                        .HasColumnType("int");

                    b.Property<string>("QuocTich")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Quoc_Tich");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaKhachHang")
                        .HasName("PK__KhachHan__37BB3C420E78E4C3");

                    b.HasIndex("MaTour");

                    b.ToTable("KhachHang", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.KhuyenMai", b =>
                {
                    b.Property<int>("MaKhuyenMai")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Khuyen_Mai");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhuyenMai"));

                    b.Property<string>("GiaGiam")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Gia_Giam");

                    b.Property<int>("MaGiaTour")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Gia_Tour");

                    b.Property<DateOnly>("NgayBatDau")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Bat_Dau");

                    b.Property<DateOnly>("NgayKetThuc")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Ket_Thuc");

                    b.Property<double?>("PhanTramGiam")
                        .HasColumnType("float")
                        .HasColumnName("Phan_Tram_Giam");

                    b.Property<string>("TenKhuyenMai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ten_Khuyen_Mai");

                    b.HasKey("MaKhuyenMai")
                        .HasName("PK__KhuyenMa__19F298C5BD513EEA");

                    b.HasIndex("MaGiaTour");

                    b.ToTable("KhuyenMai", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.LoaiTour", b =>
                {
                    b.Property<int>("MaLoaiTour")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Loai_Tour");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiTour"));

                    b.Property<string>("TenLoaiTour")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ten_Loai_Tour");

                    b.HasKey("MaLoaiTour")
                        .HasName("PK__LoaiTour__ACE9E7DAC83A2409");

                    b.ToTable("LoaiTour", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.NhanVien", b =>
                {
                    b.Property<int>("MaNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Nhan_Vien");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhanVien"));

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Hinh_Anh");

                    b.Property<string>("LinkFB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkIG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkZL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaChucVu")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Chuc_Vu");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaNhanVien")
                        .HasName("PK__NhanVien__7AB896891713CABD");

                    b.HasIndex("MaChucVu");

                    b.ToTable("NhanVien", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.NhomTour", b =>
                {
                    b.Property<int>("MaNhomTour")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Nhom_Tour");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhomTour"));

                    b.Property<string>("DiemXuatPhat")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Diem_Xuat_Phat");

                    b.Property<int>("MaTour")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Tour");

                    b.Property<int>("MaTrangThai")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Trang_Thai");

                    b.Property<DateOnly>("NgayKetThuc")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Ket_Thuc");

                    b.Property<DateOnly>("NgayKhoiHanh")
                        .HasColumnType("date")
                        .HasColumnName("Ngay_Khoi_Hanh");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Noi_Dung");

                    b.Property<string>("SoLuongNguoi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("So_Luong_Nguoi");

                    b.HasKey("MaNhomTour")
                        .HasName("PK__NhomTour__3D75DE0FA98AB25B");

                    b.HasIndex("MaTour");

                    b.HasIndex("MaTrangThai");

                    b.ToTable("NhomTour", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.PhuongTien", b =>
                {
                    b.Property<int>("MaPhuongTien")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Phuong_Tien");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPhuongTien"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Trang_Thai");

                    b.HasKey("MaPhuongTien")
                        .HasName("PK__PhuongTi__09E91370EB43D8E5");

                    b.ToTable("PhuongTien", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTermsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepeatPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.Tour", b =>
                {
                    b.Property<int>("MaTour")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Tour");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTour"));

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Hinh_Anh");

                    b.Property<int>("MaDiemDen")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Diem_Den");

                    b.Property<int>("MaGiaTour")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Gia_Tour");

                    b.Property<int>("MaLoaiTour")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Loai_Tour");

                    b.Property<string>("SoLuongNguoi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("So_Luong_Nguoi");

                    b.Property<string>("SoNgay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("So_Ngay");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaTour")
                        .HasName("PK__Tour__1C19FFEADA1259D4");

                    b.HasIndex("MaDiemDen");

                    b.HasIndex("MaGiaTour");

                    b.HasIndex("MaLoaiTour");

                    b.ToTable("Tour", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.TrangThai", b =>
                {
                    b.Property<int>("MaTrangThai")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("int")
                        .HasColumnName("Ma_Trang_Thai");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTrangThai"));

                    b.Property<string>("TenTrangThai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ten_Trang_Thai");

                    b.HasKey("MaTrangThai")
                        .HasName("PK__TrangTha__B9CD9D6BDB4AF9BE");

                    b.ToTable("TrangThai", (string)null);
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Remember")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DanhGia", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.KhachHang", "KhachHang")
                        .WithMany("DanhGias")
                        .HasForeignKey("MaKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.HoaDon", "MaHoaDonNavigation")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("MaHoaDon")
                        .IsRequired()
                        .HasConstraintName("FK__ChiTietHo__Ma_Ho__5629CD9C");

                    b.HasOne("Nhom7_webTourdulich.Models.KhachHang", "MaKhachHangNavigation")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("MaKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaHoaDonNavigation");

                    b.Navigation("MaKhachHangNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.HoaDon", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.KhachHang", "MaKhachHangNavigation")
                        .WithMany("HoaDons")
                        .HasForeignKey("MaKhachHangNavigationMaKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nhom7_webTourdulich.Models.NhomTour", "MaNhomTourNavigation")
                        .WithMany("HoaDons")
                        .HasForeignKey("MaNhomTour")
                        .IsRequired()
                        .HasConstraintName("FK__HoaDon__Ma_Nhom___52593CB8");

                    b.Navigation("MaKhachHangNavigation");

                    b.Navigation("MaNhomTourNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.KhachHang", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.Tour", "Tour")
                        .WithMany("KhachHangs")
                        .HasForeignKey("MaTour");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.KhuyenMai", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.GiaTour", "MaGiaTourNavigation")
                        .WithMany("KhuyenMais")
                        .HasForeignKey("MaGiaTour")
                        .IsRequired()
                        .HasConstraintName("FK__KhuyenMai__Ma_Gi__59FA5E80");

                    b.Navigation("MaGiaTourNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.NhanVien", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.ChucVu", "MaChucVuNavigation")
                        .WithMany("NhanViens")
                        .HasForeignKey("MaChucVu")
                        .IsRequired()
                        .HasConstraintName("FK__NhanVien__Ma_Chu__4316F928");

                    b.Navigation("MaChucVuNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.NhomTour", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.Tour", "MaTourNavigation")
                        .WithMany("NhomTours")
                        .HasForeignKey("MaTour")
                        .IsRequired()
                        .HasConstraintName("FK__NhomTour__Ma_Tou__4CA06362");

                    b.HasOne("Nhom7_webTourdulich.Models.TrangThai", "MaTrangThaiNavigation")
                        .WithMany("NhomTours")
                        .HasForeignKey("MaTrangThai")
                        .IsRequired()
                        .HasConstraintName("FK__NhomTour__Ma_Tra__4D94879B");

                    b.Navigation("MaTourNavigation");

                    b.Navigation("MaTrangThaiNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.Tour", b =>
                {
                    b.HasOne("Nhom7_webTourdulich.Models.DiemDen", "MaDiemDenNavigation")
                        .WithMany("Tours")
                        .HasForeignKey("MaDiemDen")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__Ma_Diem_De__49C3F6B7");

                    b.HasOne("Nhom7_webTourdulich.Models.GiaTour", "MaGiaTourNavigation")
                        .WithMany("Tours")
                        .HasForeignKey("MaGiaTour")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__Ma_Gia_Tou__48CFD27E");

                    b.HasOne("Nhom7_webTourdulich.Models.LoaiTour", "MaLoaiTourNavigation")
                        .WithMany("Tours")
                        .HasForeignKey("MaLoaiTour")
                        .IsRequired()
                        .HasConstraintName("FK__Tour__Ma_Loai_To__47DBAE45");

                    b.Navigation("MaDiemDenNavigation");

                    b.Navigation("MaGiaTourNavigation");

                    b.Navigation("MaLoaiTourNavigation");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.ChucVu", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.DiemDen", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.GiaTour", b =>
                {
                    b.Navigation("KhuyenMais");

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.KhachHang", b =>
                {
                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("DanhGias");

                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.LoaiTour", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.NhomTour", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.Tour", b =>
                {
                    b.Navigation("KhachHangs");

                    b.Navigation("NhomTours");
                });

            modelBuilder.Entity("Nhom7_webTourdulich.Models.TrangThai", b =>
                {
                    b.Navigation("NhomTours");
                });
#pragma warning restore 612, 618
        }
    }
}
