using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nhom7_webTourdulich.Models;

public partial class ChucVu
{
    [Key]
    public string MaChucVu { get; set; } = null!;

    public string TenChucVu { get; set; } = null!;

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
