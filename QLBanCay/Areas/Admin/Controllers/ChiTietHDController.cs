using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ChiTietHDController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("ChiTietHD")]
        public IActionResult ChiTietHD(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var dshd = csdl.ChiTietHdbs.AsNoTracking().OrderBy(x => x.MaChiTiet);
            PagedList<ChiTietHdb> paged_dshd = new PagedList<ChiTietHdb>(dshd, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_dshd);
        }

        [Route("Themchitiet")]
        [HttpGet]
        public IActionResult Themchitiet()
        {
            ViewBag.MaHdb = new SelectList(csdl.HoaDonBans.ToList(), "MaHdb", "MaKh");
            ViewBag.MaChiTiet = new SelectList(csdl.ChiTietCays.ToList(), "MaChiTiet", "MaCay");
            return View();
        }

        [Route("Themchitiet")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Themchitiet(ChiTietHdb hd)
        {
            if (ModelState.IsValid)
            {
                csdl.ChiTietHdbs.Add(hd);
                csdl.SaveChanges();
                return RedirectToAction("ChiTietHD");
            }
            return View(hd);
        }

        [Route("SuaCTHD")]
        [HttpGet]
        public IActionResult SuaCTHD(string maCT, string MaHDB)
        {
            ViewBag.MaHdb = new SelectList(csdl.HoaDonBans.ToList(), "MaHdb", "MaKh");
            ViewBag.MaChiTiet = new SelectList(csdl.ChiTietCays.ToList(), "MaChiTiet", "MaCay");
            var chitiet = csdl.ChiTietHdbs.Find(MaHDB, maCT);
            return View(chitiet);
        }

        [Route("SuaCTHD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTHD(ChiTietHdb chitiet)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(chitiet);
                /*csdl.Entry(chitiet).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("ChiTietHD");
            }
            return View(chitiet);
        }

        [Route("Xoachitiet")]
        [HttpGet]
        public IActionResult Xoachitiet(string maCT, string MaHDB)
        {
            TempData["Message"] = "";            
            csdl.Remove(csdl.ChiTietHdbs.Find(MaHDB, maCT));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá chi tiết hoá đơn thành công";
            return RedirectToAction("ChiTietHD");

        }
    }
}
