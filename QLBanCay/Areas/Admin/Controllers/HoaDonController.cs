using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Helpers;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HoaDonController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhsachHD")]
        public IActionResult DanhsachHD(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var dshd = csdl.HoaDonBans.AsNoTracking().OrderBy(x => x.MaHdb);
            PagedList<HoaDonBan> paged_dshd = new PagedList<HoaDonBan>(dshd, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_dshd);
        }

        [Route("Themhoadon")]
        [HttpGet]
        public IActionResult ThemHoaDon()
        {
            ViewBag.Makh = new SelectList(csdl.KhachHangs.ToList(), "MaKh", "TenKh");
            ViewBag.Manv = new SelectList(csdl.NhanViens.ToList(), "MaNv", "TenNv");
            return View();
        }

        [Route("Themhoadon")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHoaDon(HoaDonBan hd)
        {
            if (ModelState.IsValid)
            {
                csdl.HoaDonBans.Add(hd);
                csdl.SaveChanges();
                return RedirectToAction("DanhsachHD");
            }
            return View(hd);
        }

        [Route("SuaHD")]
        [HttpGet]
        public IActionResult SuaHD(string maHD)
        {
            ViewBag.Makh = new SelectList(csdl.KhachHangs.ToList(), "MaKh", "TenKh");
            ViewBag.Manv = new SelectList(csdl.NhanViens.ToList(), "MaNv", "TenNv");
            var hoadon = csdl.HoaDonBans.Find(maHD);
            return View(hoadon);
        }

        [Route("SuaHD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHD(HoaDonBan hoadon)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(hoadon);
                /*csdl.Entry(hoadon).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("DanhsachHD");
            }
            return View(hoadon);
        }

        [Route("Xoahoadon")]
        [HttpGet]
        public IActionResult Xoahoadon(string maHD)
        {
            TempData["Message"] = "";
            var chiTietHD = csdl.ChiTietHdbs.Where(x => x.MaHdb == maHD).ToList();
            if (chiTietHD.Count() > 0)
            {
                TempData["Message"] = "Không xoá được hoá đơn này";
                return RedirectToAction("DanhsachHD");
            }
            
            csdl.Remove(csdl.HoaDonBans.Find(maHD));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá hoá đơn thành công";
            return RedirectToAction("DanhsachHD");

        }

        
    }
}
