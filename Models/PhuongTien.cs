using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class PhuongTien
{
    public string MaPhuongTien { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietNhomTour> ChiTietNhomTours { get; set; } = new List<ChiTietNhomTour>();
}
