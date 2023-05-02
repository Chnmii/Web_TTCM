using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class ChiTietHdb
{
    public string MaHdb { get; set; } 

    public string MaChiTiet { get; set; } 

    public int Slban { get; set; }

    public double? DonGia { get; set; }

    public string? GiamGia { get; set; }

    public virtual ChiTietCay? MaChiTietNavigation { get; set; }

    public virtual HoaDonBan? MaHdbNavigation { get; set; } 
}
