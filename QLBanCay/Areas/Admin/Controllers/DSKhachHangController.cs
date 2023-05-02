using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DSKhachHangController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/DanhsachKH")]
        public IActionResult DanhsachKH(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var dskh = csdl.KhachHangs.AsNoTracking().OrderBy(x => x.TenKh);
            PagedList<KhachHang> paged_dskh = new PagedList<KhachHang>(dskh, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_dskh);
        }

        [Route("Themkhachhang")]
        [HttpGet]
        public IActionResult ThemKHMoi()
        {
            ViewBag.UserName = new SelectList(csdl.Users.ToList(), "UserName", "LoaiUser");
            return View();
        }

        [Route("Themkhachhang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKHMoi(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                csdl.KhachHangs.Add(kh);
                csdl.SaveChanges();
                return RedirectToAction("DanhsachKH");
            }
            return View(kh);
        }

        [Route("Suakhachhang")]
        [HttpGet]
        public IActionResult SuaKhachhang(string makh)
        {
            ViewBag.UserName = new SelectList(csdl.Users.ToList(), "UserName", "LoaiUser");
            var kh = csdl.KhachHangs.Find(makh);
            return View(kh);
        }

        [Route("Suakhachhang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachhang(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(kh);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("DanhsachKH");
            }
            return View(kh);
        }

        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(string maKH)
        {                
            
            TempData["Message"] = "";
            var chiTietHD = csdl.HoaDonBans.Where(x => x.MaKh == maKH).ToList();
            if (chiTietHD.Count() > 0)
            {
                TempData["Message"] = "Không xoá được khách hàng này";
                return RedirectToAction("DanhsachNV");
            }
            csdl.Remove(csdl.KhachHangs.Find(maKH));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá khách hàng thành công";
            return RedirectToAction("DanhsachKH");

        }

    }
}
