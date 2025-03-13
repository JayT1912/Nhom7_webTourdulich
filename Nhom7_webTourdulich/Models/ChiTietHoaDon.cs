using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class ChiTietHoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public string MaKhachHang { get; set; } = null!;

    public decimal GiaTour { get; set; }

    public int SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
