using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class DiemDen
{
    public string MaDiemDen { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string ThanhPho { get; set; } = null!;

    public virtual ICollection<KhachSan> KhachSans { get; set; } = new List<KhachSan>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
