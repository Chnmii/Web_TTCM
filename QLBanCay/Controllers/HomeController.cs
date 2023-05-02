using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using QLBanCay.Models.Authentication;
using QLBanCay.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace QLBanCay.Controllers
{
	public class HomeController : Controller
	{
		QlbanCayContext csdl = new QlbanCayContext();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

        
		public IActionResult Index(int? page)
		{
            int pageSize = 4;
            // so luong trang
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanpham = csdl.DanhMucCays.AsNoTracking().OrderBy(x => x.TenCay);
            // phan trang
            PagedList<DanhMucCay> list = new PagedList<DanhMucCay>(listSanpham, pageNumber, pageSize);
            return View(list);

        }

        public IActionResult SanPhamTheoLoai(string MaLoai)
        {
            // home/SanPhamTheoLoai?maloai=vali (tui, balo...) 
            var lstsanpham = csdl.DanhMucCays.Where(x => x.MaLoai == MaLoai).OrderBy(x => x.TenCay).ToList();
            ViewBag.maloai = MaLoai;
            return View(lstsanpham);
        }

        public IActionResult ProductDetail(String masp)
        {
            var sanpham = csdl.DanhMucCays.SingleOrDefault(x => x.MaCay == masp);
            var chitietsp = csdl.ChiTietCays.SingleOrDefault(x => x.MaCay == masp);
            var homeProductDetail = new HomeProductDetailViewModel
            {
                danhMucSp = sanpham,
				chiTietCay = chitietsp
            };
            return View(homeProductDetail);
        }
        
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}