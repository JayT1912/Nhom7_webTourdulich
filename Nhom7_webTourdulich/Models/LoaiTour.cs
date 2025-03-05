using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class LoaiTour
{
    public string MaLoaiTour { get; set; } = null!;

    public string TenLoaiTour { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
