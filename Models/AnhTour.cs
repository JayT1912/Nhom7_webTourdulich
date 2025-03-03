using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class AnhTour
{
    public int MaAnh { get; set; }

    public string MaTour { get; set; } = null!;

    public string LoaiFile { get; set; } = null!;

    public byte[] HinhAnh { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
