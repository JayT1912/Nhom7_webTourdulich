using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string MaVaiTro { get; set; } = null!;

    public virtual ICollection<ChiTietNhomTour> ChiTietNhomTours { get; set; } = new List<ChiTietNhomTour>();

    public virtual ChucVu MaVaiTroNavigation { get; set; } = null!;
}
