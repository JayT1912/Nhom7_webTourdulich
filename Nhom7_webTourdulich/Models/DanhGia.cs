using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Models;

public class DanhGia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaDanhGia { get; set; } // Khóa chính
    public string NoiDung { get; set; } = null!;
    public DateTime NgayDanhGia { get; set; }
    public string Image { get; set; } = null!; // Đường dẫn ảnh
    
    [ForeignKey("KhachHang")]
    public int MaKhachHang { get; set; }  // Khách hàng liên quan đến đánh giá
    public virtual KhachHang KhachHang { get; set; } = null!;
}
