using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class HoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public string MaKhachHang { get; set; } = null!;

    public string MaNhomTour { get; set; } = null!;

    public DateOnly NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public string DiemDon { get; set; } = null!;

    public DateOnly NgayDi { get; set; }

    public TimeOnly GioDi { get; set; }

    public string ThanhToan { get; set; } = null!;

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;

    public virtual NhomTour MaNhomTourNavigation { get; set; } = null!;
}
