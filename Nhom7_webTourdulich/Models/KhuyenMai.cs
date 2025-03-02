using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class KhuyenMai
{
    public string MaKhuyenMai { get; set; } = null!;

    public string TenKhuyenMai { get; set; } = null!;

    public string MaGiaTour { get; set; } = null!;

    public string? PhanTramGiam { get; set; }

    public string? GiaGiam { get; set; }

    public string NgayBatDau { get; set; } = null!;

    public string NgayKetThuc { get; set; } = null!;

    public virtual GiaTour MaGiaTourNavigation { get; set; } = null!;
}
