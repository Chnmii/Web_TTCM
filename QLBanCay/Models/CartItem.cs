namespace QLBanCay.Models
{
    public class CartItem
    {
        public string? MaCay { get; set; }
        public string? TenCay { get; set; }
        public string? Anh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien => SoLuong * DonGia;

    }
}
