using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GioiTinh { get; set; } = null!;

    public string QuocTich { get; set; } = null!;

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<HanhKhach> HanhKhaches { get; set; } = new List<HanhKhach>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
