using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class LoaiHanhKhach
{
    public string MaLoaiKhach { get; set; } = null!;

    public string TenLoaiKhach { get; set; } = null!;

    public virtual ICollection<HanhKhach> HanhKhaches { get; set; } = new List<HanhKhach>();
}
