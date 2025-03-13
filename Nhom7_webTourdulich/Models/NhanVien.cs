using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models;

public partial class NhanVien
{
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaNhanVien { get; set; } 

    public string Ten { get; set; } = null!;

    public string MaChucVu { get; set; } = null!;

    public string? HinhAnh { get; set; }
<<<<<<< HEAD
    public string? LinkFB { get; set; }
    public string? LinkZL { get; set; }
    public string? LinkIG { get; set; }
=======

>>>>>>> b5310ada0464d2eebf8d9a629d950c4f1ee8efa5
    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;
}
