using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class NhomTour
{
    public string MaNhomTour { get; set; } = null!;

    public string MaTour { get; set; } = null!;

    public string NgayKhoiHanh { get; set; } = null!;

    public string NgayKetThuc { get; set; } = null!;

    public string DiemXuatPhat { get; set; } = null!;

    public string MaTrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietNhomTour> ChiTietNhomTours { get; set; } = new List<ChiTietNhomTour>();

    public virtual ICollection<HanhKhach> HanhKhaches { get; set; } = new List<HanhKhach>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual Tour MaTourNavigation { get; set; } = null!;

    public virtual TrangThai MaTrangThaiNavigation { get; set; } = null!;
}
