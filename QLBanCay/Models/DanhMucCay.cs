using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class DanhMucCay
{
    public string MaCay { get; set; } = null!;

    public string TenCay { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public string? MoTa { get; set; }

    public string MaNcc { get; set; } = null!;

    public string MaNuocSx { get; set; } = null!;

    public string? CachChamSoc { get; set; }

    public string? AnhCay { get; set; }

    public virtual ICollection<ChiTietCay> ChiTietCays { get; set; } = new List<ChiTietCay>();

    public virtual NhaCungCap? MaNccNavigation { get; set; } 
    public virtual NuocSanXuat? MaNuocSxNavigation { get; set; } 
}
