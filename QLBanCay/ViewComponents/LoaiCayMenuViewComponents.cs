using Microsoft.AspNetCore.Mvc;
using QLBanCay.Repository;

namespace QLBanCay.ViewComponents
{
    public class LoaiCayMenuViewComponent : ViewComponent
    {
        private readonly ILoaiCayRepository _loaiCay;
        public LoaiCayMenuViewComponent(ILoaiCayRepository loaiSpRepository)
        {
            _loaiCay = loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            var AllLoaiCay = _loaiCay.GetAllLoaiCay().OrderBy(x => x.TenLoai);
            return View(AllLoaiCay);
        }
    }


}
