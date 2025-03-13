using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class GiaTour
{
    public string MaGiaTour { get; set; } = null!;

    public decimal Gia { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
