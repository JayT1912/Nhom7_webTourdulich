﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Ma_Chuc_Vu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten_Chuc_Vu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucVu__EFE449720DAF3C2C", x => x.Ma_Chuc_Vu);
                });

            migrationBuilder.CreateTable(
                name: "DiemDen",
                columns: table => new
                {
                    Ma_Diem_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Thanh_Pho = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiemDen__34267B5EDBE1CB85", x => x.Ma_Diem_Den);
                });

            migrationBuilder.CreateTable(
                name: "GiaTour",
                columns: table => new
                {
                    Ma_Gia_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Gia = table.Column<decimal>(type: "money", nullable: false),
                    Ngay_Bat_Dau = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaTour__6B20AB462CA1B26C", x => x.Ma_Gia_Tour);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTour",
                columns: table => new
                {
                    Ma_Loai_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten_Loai_Tour = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiTour__ACE9E7DA26904217", x => x.Ma_Loai_Tour);
                });

            migrationBuilder.CreateTable(
                name: "PhuongTien",
                columns: table => new
                {
                    Ma_Phuong_Tien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Trang_Thai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuongTi__09E91370F7B68465", x => x.Ma_Phuong_Tien);
                });

            migrationBuilder.CreateTable(
                name: "TrangThai",
                columns: table => new
                {
                    Ma_Trang_Thai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten_Trang_Thai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__B9CD9D6BA20F14FE", x => x.Ma_Trang_Thai);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Ma_Nhan_Vien = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Chuc_Vu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Hinh_Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LinkFB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkZL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkIG = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__7AB89689143E91C2", x => x.Ma_Nhan_Vien);
                    table.ForeignKey(
                        name: "FK__NhanVien__Ma_Chu__5812160E",
                        column: x => x.Ma_Chuc_Vu,
                        principalTable: "ChucVu",
                        principalColumn: "Ma_Chuc_Vu");
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    Ma_Khuyen_Mai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten_Khuyen_Mai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Gia_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Phan_Tram_Giam = table.Column<double>(type: "float", nullable: true),
                    Gia_Giam = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Ngay_Bat_Dau = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhuyenMa__19F298C55EA7F7FF", x => x.Ma_Khuyen_Mai);
                    table.ForeignKey(
                        name: "FK__KhuyenMai__Ma_Gi__6C190EBB",
                        column: x => x.Ma_Gia_Tour,
                        principalTable: "GiaTour",
                        principalColumn: "Ma_Gia_Tour");
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Ma_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Loai_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Gia_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Diem_Den = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    So_Ngay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    So_Luong_Nguoi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Hinh_Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tour__1C19FFEA992E3B6A", x => x.Ma_Tour);
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Diem_De__76969D2E",
                        column: x => x.Ma_Diem_Den,
                        principalTable: "DiemDen",
                        principalColumn: "Ma_Diem_Den");
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Gia_Tou__75A278F5",
                        column: x => x.Ma_Gia_Tour,
                        principalTable: "GiaTour",
                        principalColumn: "Ma_Gia_Tour");
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Loai_To__74AE54BC",
                        column: x => x.Ma_Loai_Tour,
                        principalTable: "LoaiTour",
                        principalColumn: "Ma_Loai_Tour");
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sdt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Dia_Chi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gioi_Tinh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Quoc_Tich = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hinh_Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaTour = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__37BB3C42DB802C2D", x => x.Ma_Khach_Hang);
                    table.ForeignKey(
                        name: "FK_KhachHang_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour");
                });

            migrationBuilder.CreateTable(
                name: "NhomTour",
                columns: table => new
                {
                    Ma_Nhom_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ngay_Khoi_Hanh = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false),
                    Diem_Xuat_Phat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Trang_Thai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    So_Luong_Nguoi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Noi_Dung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhomTour__3D75DE0FE9D64DA2", x => x.Ma_Nhom_Tour);
                    table.ForeignKey(
                        name: "FK__NhomTour__Ma_Tou__0B91BA14",
                        column: x => x.Ma_Tour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour");
                    table.ForeignKey(
                        name: "FK__NhomTour__Ma_Tra__0C85DE4D",
                        column: x => x.Ma_Trang_Thai,
                        principalTable: "TrangThai",
                        principalColumn: "Ma_Trang_Thai");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Ma_Hoa_Don = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ma_Nhom_Tour = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ngay_Lap = table.Column<DateOnly>(type: "date", nullable: false),
                    Tong_Tien = table.Column<decimal>(type: "money", nullable: false),
                    Diem_Don = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ngay_Di = table.Column<DateOnly>(type: "date", nullable: false),
                    Gio_Di = table.Column<TimeOnly>(type: "time", nullable: false),
                    Thanh_Toan = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__91EF063FC4157924", x => x.Ma_Hoa_Don);
                    table.ForeignKey(
                        name: "FK__HoaDon__Ma_Khach__10566F31",
                        column: x => x.Ma_Khach_Hang,
                        principalTable: "KhachHang",
                        principalColumn: "Ma_Khach_Hang");
                    table.ForeignKey(
                        name: "FK__HoaDon__Ma_Nhom___114A936A",
                        column: x => x.Ma_Nhom_Tour,
                        principalTable: "NhomTour",
                        principalColumn: "Ma_Nhom_Tour");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    Ma_Hoa_Don = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Gia_Tour = table.Column<decimal>(type: "money", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Thanh_Tien = table.Column<decimal>(type: "money", nullable: true, computedColumnSql: "([Gia_Tour]*[So_Luong])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietH__A294B5FBC6D9C49E", x => new { x.Ma_Hoa_Don, x.Ma_Khach_Hang });
                    table.ForeignKey(
                        name: "FK__ChiTietHo__Ma_Ho__151B244E",
                        column: x => x.Ma_Hoa_Don,
                        principalTable: "HoaDon",
                        principalColumn: "Ma_Hoa_Don");
                    table.ForeignKey(
                        name: "FK__ChiTietHo__Ma_Kh__160F4887",
                        column: x => x.Ma_Khach_Hang,
                        principalTable: "KhachHang",
                        principalColumn: "Ma_Khach_Hang");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_Ma_Khach_Hang",
                table: "ChiTietHoaDon",
                column: "Ma_Khach_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Ma_Khach_Hang",
                table: "HoaDon",
                column: "Ma_Khach_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Ma_Nhom_Tour",
                table: "HoaDon",
                column: "Ma_Nhom_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaTour",
                table: "KhachHang",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMai_Ma_Gia_Tour",
                table: "KhuyenMai",
                column: "Ma_Gia_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_Ma_Chuc_Vu",
                table: "NhanVien",
                column: "Ma_Chuc_Vu");

            migrationBuilder.CreateIndex(
                name: "IX_NhomTour_Ma_Tour",
                table: "NhomTour",
                column: "Ma_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_NhomTour_Ma_Trang_Thai",
                table: "NhomTour",
                column: "Ma_Trang_Thai");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Diem_Den",
                table: "Tour",
                column: "Ma_Diem_Den");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Gia_Tour",
                table: "Tour",
                column: "Ma_Gia_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Loai_Tour",
                table: "Tour",
                column: "Ma_Loai_Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhuongTien");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhomTour");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "TrangThai");

            migrationBuilder.DropTable(
                name: "DiemDen");

            migrationBuilder.DropTable(
                name: "GiaTour");

            migrationBuilder.DropTable(
                name: "LoaiTour");
        }
    }
}
