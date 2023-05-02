﻿using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class HoaDonBan
{
    public string MaHdb { get; set; } = null!;

    public DateTime? NgayBan { get; set; }

    public string? MaKh { get; set; }

    public string? MaNv { get; set; }

    public double? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
