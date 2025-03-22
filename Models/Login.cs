using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nhom7_webTourdulich.Models;


public class LoginModel
{
    [Key]
    public string id {get ; set;}

    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
