using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenNcc { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<DanhMucCay> DanhMucCays { get; set; } = new List<DanhMucCay>();
}
