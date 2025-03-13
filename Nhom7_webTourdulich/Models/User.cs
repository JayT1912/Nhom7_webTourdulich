using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nhom7_webTourdulich.Models;
public partial class User
    {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }

    public bool Remember { get; set; }
    }