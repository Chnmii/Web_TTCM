using QLBanCay.Models;

namespace QLBanCay.Repository
{
    public class LoaiCayRepository : ILoaiCayRepository
    {
        private readonly QlbanCayContext _context;

        public LoaiCayRepository(QlbanCayContext context)
        {
            _context = context;
        }

        public LoaiCay Add(LoaiCay LoaiCay)
        {
            _context.LoaiCays.Add(LoaiCay);
            _context.SaveChanges();
            return LoaiCay;

        }

        public LoaiCay Delete(string maLoaiCay)
        {
            throw new NotImplementedException();
        }

        public LoaiCay GeLoaiCay(string maLoaiCay)
        {
            return _context.LoaiCays.Find(maLoaiCay);
        }

        public IEnumerable<LoaiCay> GetAllLoaiCay()
        {
            return _context.LoaiCays;
        }

        public LoaiCay Update(LoaiCay LoaiCay)
        {
            _context.Update(LoaiCay);
            _context.SaveChanges();
            return LoaiCay;

        }
    }
}
