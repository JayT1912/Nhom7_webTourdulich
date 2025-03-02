using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class HanhKhach
{
    public string MaKhachHang { get; set; } = null!;

    public string MaLoaiKhach { get; set; } = null!;

    public string MaNhomTour { get; set; } = null!;

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;

    public virtual LoaiHanhKhach MaLoaiKhachNavigation { get; set; } = null!;

    public virtual NhomTour MaNhomTourNavigation { get; set; } = null!;
}
