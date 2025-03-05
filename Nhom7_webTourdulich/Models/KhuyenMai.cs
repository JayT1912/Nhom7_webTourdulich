using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class KhuyenMai
{
    public string MaKhuyenMai { get; set; } = null!;

    public string TenKhuyenMai { get; set; } = null!;

    public string MaGiaTour { get; set; } = null!;

    public double? PhanTramGiam { get; set; }

    public string? GiaGiam { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual GiaTour MaGiaTourNavigation { get; set; } = null!;
}
