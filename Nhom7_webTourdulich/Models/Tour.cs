﻿using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class Tour
{
    public string MaTour { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string MaLoaiTour { get; set; } = null!;

    public string MaGiaTour { get; set; } = null!;

    public string MaDiemDen { get; set; } = null!;

    public int SoNgay { get; set; }

    public string? SoLuongNguoi { get; set; }

    public string? HinhAnh { get; set; }

    public virtual DiemDen MaDiemDenNavigation { get; set; } = null!;

    public virtual GiaTour MaGiaTourNavigation { get; set; } = null!;
    public virtual LoaiTour MaLoaiTourNavigation { get; set; } = null!;

    public virtual ICollection<NhomTour> NhomTours { get; set; } = new List<NhomTour>();
}
