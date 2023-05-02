using Microsoft.AspNetCore.Mvc;
using QLBanCay.Models;
using QLBanCay.Helpers;

namespace QLBanCay.Controllers
{
    public class ShopcartController : Controller
    {
        private readonly QlbanCayContext _context;

        public ShopcartController(QlbanCayContext context)
        {
            _context = context;
        }

        public List<CartItem> Cart
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Giohang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }    
                return data;
            }
        }

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(string id, int SoLuong)
        {
            var mycart = Cart;
            var item = mycart.SingleOrDefault(p => p.MaCay == id);
            if (item == null)
            {
                var chitiet = _context.ChiTietCays.SingleOrDefault(p => p.MaCay == id);
                var sanpham = _context.DanhMucCays.SingleOrDefault(p => p.MaCay == id);
                item = new CartItem
                {
                    MaCay = id,
                    TenCay = sanpham.TenCay,
                    Anh = sanpham.AnhCay,
                    DonGia = chitiet.DonGia,
                    SoLuong = SoLuong                 

                };
                mycart.Add(item);

            }
            else
            {
                item.SoLuong += SoLuong;
                
            }
            HttpContext.Session.Set("Giohang", mycart);
            return RedirectToAction("Index");
        }

        
    }
}
