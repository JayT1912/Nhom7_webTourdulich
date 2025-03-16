using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;

public partial class PhuongTien
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string MaPhuongTien { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string TrangThai { get; set; } = null!;
}
