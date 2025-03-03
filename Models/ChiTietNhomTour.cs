using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class ChiTietNhomTour
{
    public string MaNhomTour { get; set; } = null!;

    public string MaNhanVien { get; set; } = null!;

    public string MaPhuongTien { get; set; } = null!;

    public virtual NhanVien MaNhanVienNavigation { get; set; } = null!;

    public virtual NhomTour MaNhomTourNavigation { get; set; } = null!;

    public virtual PhuongTien MaPhuongTienNavigation { get; set; } = null!;
}
