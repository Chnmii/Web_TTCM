using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? LoaiUser { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
