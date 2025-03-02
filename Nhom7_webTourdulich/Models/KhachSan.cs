using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class KhachSan
{
    public string MaKhachSan { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public string MaDiemDen { get; set; } = null!;

    public virtual DiemDen MaDiemDenNavigation { get; set; } = null!;
}
