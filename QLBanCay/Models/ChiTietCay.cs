using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class ChiTietCay
{
    public string MaChiTiet { get; set; } = null!;

    public string MaCay { get; set; } = null!;

    public string? AnhCay { get; set; }

    public double DonGia { get; set; }

    public int Slton { get; set; }

    public virtual AnhChiTiet? AnhChiTiet { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual DanhMucCay MaCayNavigation { get; set; } = null!;
}
