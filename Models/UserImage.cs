﻿namespace Nhom7_webTourdulich.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }

}
