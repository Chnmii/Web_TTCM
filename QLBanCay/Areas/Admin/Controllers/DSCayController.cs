using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DSCayController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucCay")]
        public IActionResult DanhMucCay(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var dscay = csdl.DanhMucCays.AsNoTracking().OrderBy(x => x.TenCay);
            PagedList<DanhMucCay> paged_dscay = new PagedList<DanhMucCay>(dscay, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_dscay);
        }

        [Route("ThemCayMoi")]
        [HttpGet]
        public IActionResult ThemCayMoi()
        {
            ViewBag.MaNcc = new SelectList(csdl.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.MaNuocSx = new SelectList(csdl.NuocSanXuats.ToList(), "MaNuocSx", "TenNuocSx");
            return View();
        }

        [Route("ThemCayMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCayMoi(DanhMucCay cay)
        {
            if (ModelState.IsValid)
            {
                csdl.DanhMucCays.Add(cay);
                csdl.SaveChanges();
                return RedirectToAction("DanhMucCay");
            }
            return View(cay);
        }

        [Route("SuaCay")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSp)
        {
            ViewBag.MaNcc = new SelectList(csdl.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.MaNuocSx = new SelectList(csdl.NuocSanXuats.ToList(), "MaNuocSx", "TenNuocSx");
            var sanpham = csdl.DanhMucCays.Find(maSp);
            return View(sanpham);
        }

        [Route("SuaCay")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(DanhMucCay sanpham)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(sanpham);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("DanhMucCay");
            }
            return View(sanpham);
        }

        [Route("XoaCay")]
        [HttpGet]
        public IActionResult XoaCay(string maCay)
        {
            TempData["Message"] = "";
            var chiTietSp = csdl.ChiTietCays.Where(x => x.MaCay == maCay).ToList();
            if (chiTietSp.Count() > 0)
            {
                TempData["Message"] = "Không xoá được sản phẩm này";
                return RedirectToAction("Danhmuccay");
            }
;
            csdl.Remove(csdl.DanhMucCays.Find(maCay));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá sản phẩm thành công";
            return RedirectToAction("Danhmuccay");

        }


    }
}
