using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string MaChucVu { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;
}
