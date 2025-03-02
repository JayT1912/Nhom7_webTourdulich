using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class GiaTour
{
    public string MaGiaTour { get; set; } = null!;

    public string Gia { get; set; } = null!;

    public string NgayBatDau { get; set; } = null!;

    public string NgayKetThuc { get; set; } = null!;

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
