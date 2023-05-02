using QLBanCay.Models;
namespace QLBanCay.Repository
{
    public interface ILoaiCayRepository
    {
        LoaiCay Add(LoaiCay LoaiCay);

        LoaiCay Update(LoaiCay LoaiCay);

        LoaiCay Delete(String maLoaiCay);

        LoaiCay GeLoaiCay(String maLoaiCay);

        IEnumerable<LoaiCay> GetAllLoaiCay();

    }
}
