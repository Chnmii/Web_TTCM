using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string TenKh { get; set; } = null!;

    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual User? UserNameNavigation { get; set; }
}
