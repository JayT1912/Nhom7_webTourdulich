using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class HoaDon
{
    public string MaHoaDon { get; set; } = null!;

    public string MaKhachHang { get; set; } = null!;

    public string MaNhomTour { get; set; } = null!;

    public string NgayLap { get; set; } = null!;

    public string TongTien { get; set; } = null!;

    public string DaThanhToan { get; set; } = null!;

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;

    public virtual NhomTour MaNhomTourNavigation { get; set; } = null!;
}
