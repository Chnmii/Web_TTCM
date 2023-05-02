using System;
using System.Collections.Generic;

namespace QLBanCay.Models;

public partial class NuocSanXuat
{
    public string MaNuocSx { get; set; } = null!;

    public string TenNuocSx { get; set; } = null!;

    public virtual ICollection<DanhMucCay> DanhMucCays { get; set; } = new List<DanhMucCay>();
}
