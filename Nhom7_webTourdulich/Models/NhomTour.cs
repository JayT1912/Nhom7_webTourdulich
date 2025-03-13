using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class NhomTour
{
    public string MaNhomTour { get; set; } = null!;

    public string MaTour { get; set; } = null!;

    public DateOnly NgayKhoiHanh { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public string DiemXuatPhat { get; set; } = null!;

    public string MaTrangThai { get; set; } = null!;

    public string? SoLuongNguoi { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual Tour MaTourNavigation { get; set; } = null!;

    public virtual TrangThai MaTrangThaiNavigation { get; set; } = null!;
}
