using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanCay.Models;
using X.PagedList;

namespace QLBanCay.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NuocSXController : Controller
    {
        QlbanCayContext csdl = new QlbanCayContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("NuocSanXuat")]
        public IActionResult NuocSanXuat(int? page)
        {
            //Số đối tượng hiển thị tối đa trên 1 trang
            int pageSize = 3;
            //Số trang hiện tại đang truy cập
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            //Khởi tạo 1 danh sách tự do
            var nsx = csdl.NuocSanXuats.AsNoTracking().OrderBy(x => x.MaNuocSx);
            PagedList<NuocSanXuat> paged_nsx = new PagedList<NuocSanXuat>(nsx, pageNumber, pageSize);
            //Khai báo 1 paged danh sách 
            return View(paged_nsx);
        }

        [Route("Themnuocsx")]
        [HttpGet]
        public IActionResult ThemNuocSX()
        {

            return View();
        }

        [Route("Themnuocsx")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNuocSX(NuocSanXuat nsx)
        {
            if (ModelState.IsValid)
            {
                csdl.NuocSanXuats.Add(nsx);
                csdl.SaveChanges();
                return RedirectToAction("NuocSanXuat");
            }
            return View(nsx);
        }

        [Route("SuaNSX")]
        [HttpGet]
        public IActionResult SuaNSX(string mansx)
        {
            var NSX = csdl.NuocSanXuats.Find(mansx);
            return View(NSX);
        }

        [Route("SuaNSX")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNSX(NuocSanXuat NSX)
        {
            if (ModelState.IsValid)
            {
                csdl.Update(NSX);
                /*csdl.Entry(sanpham).State = EntityState.Modified;*/
                csdl.SaveChanges();
                return RedirectToAction("NuocSanXuat");
            }
            return View(NSX);
        }

        [Route("XoaNSX")]
        [HttpGet]
        public IActionResult XoaNSX(string maNSX)
        {
            TempData["Message"] = "";
            csdl.Remove(csdl.NuocSanXuats.Find(maNSX));
            csdl.SaveChanges();
            TempData["Message"] = "Đã xoá nước sản xuất thành công";
            return RedirectToAction("NuocSanXuat");

        }
    }
}
