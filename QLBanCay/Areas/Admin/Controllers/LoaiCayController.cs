using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LoaiCayController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("Loaicay")]
        public IActionResult LoaiCay(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //LCởi tạo 1 danh sách tự do
            var loai = csdl.LoaiCays.AsNoTracking().OrderBy(x => x.MaLoai);
            PagedList<LoaiCay> paged_loai = new PagedList<LoaiCay>(loai, pageNumber, pageSize);
            //LCai báo 1 paged danh sách 
            return View(paged_loai);
        }

        [Route("Themloaicay")]
        [HttpGet]
        public IActionResult ThemLoaiCay()
        {

            return View();
        }

        [Route("Themloaicay")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiCay(LoaiCay loai)
        {
            if (ModelState.IsValid)
            {
                csdl.LoaiCays.Add(loai);
                csdl.SaveChanges();
                return RedirectToAction("Loaicay");
            }
            return View(loai);
        }

        [Route("SuaLoaiCay")]
        [HttpGet]
        public IActionResult SuaLoaiCay(string malc)
        {
            var lc = csdl.LoaiCays.Find(malc);
            return View(lc);
        }

        [Route("SuaLoaiCay")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiCay(LoaiCay lc)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(lc);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("Loaicay");
            }
            return View(lc);
        }

        [Route("XoaLoaiCay")]
        [HttpGet]
        public IActionResult XoaLoaiCay(string maLC)
        {
            TempData["Message"] = "";
            csdl.Remove(csdl.LoaiCays.Find(maLC));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá loại cây thành công";
            return RedirectToAction("Loaicay");

        }
    }
}
