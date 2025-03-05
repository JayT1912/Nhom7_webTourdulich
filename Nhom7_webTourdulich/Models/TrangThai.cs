using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class TrangThai
{
    public string MaTrangThai { get; set; } = null!;

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<NhomTour> NhomTours { get; set; } = new List<NhomTour>();
}
