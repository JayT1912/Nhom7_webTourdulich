using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class AnhTour
{
    public int MaAnh { get; set; }

    public string MaTour { get; set; } = null!;

    public string LoaiFile { get; set; } = null!;

<<<<<<< HEAD
    public byte[] HinhAnh { get; set; } = null!;
=======
    public string HinhAnh { get; set; } = null!;
>>>>>>> 0f4116f8f5f784e210a9fdb167874b3aa19f78f6

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
