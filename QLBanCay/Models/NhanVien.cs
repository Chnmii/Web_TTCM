using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? UserName { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? ViTri { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual User? UserNameNavigation { get; set; }
}
