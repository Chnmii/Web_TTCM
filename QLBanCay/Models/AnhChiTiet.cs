using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class AnhChiTiet
{
    public string MaChiTiet { get; set; } = null!;

    public string DuongDanAnh { get; set; } = null!;

    public virtual ChiTietCay MaChiTietNavigation { get; set; } = null!;
}
