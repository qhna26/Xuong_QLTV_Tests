using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Xuong;
using DTO_Xuong;

namespace QLTV.Tests.TestDoubles
{
    public class FakeNhanVienRepository : INhanVienRepository
    {
        private readonly List<NhanVien> _nhanViens = new();

        public bool Add(NhanVien nv)
        {
            if (_nhanViens.Any(x => x.MaNhanVien == nv.MaNhanVien || x.Email == nv.Email))
                return false;

            _nhanViens.Add(nv);
            return true;
        }

        public bool Delete(string maNhanVien)
        {
            var nv = _nhanViens.FirstOrDefault(x => x.MaNhanVien == maNhanVien);
            if (nv == null) return false;
            _nhanViens.Remove(nv);
            return true;
        }

        public bool Update(NhanVien nv)
        {
            var existing = _nhanViens.FirstOrDefault(x => x.MaNhanVien == nv.MaNhanVien);
            if (existing == null) return false;

            existing.Ten = nv.Ten;
            existing.Email = nv.Email;
            existing.MatKhau = nv.MatKhau;
            return true;
        }

        public NhanVien GetById(string maNhanVien)
        {
            return _nhanViens.FirstOrDefault(x => x.MaNhanVien == maNhanVien);
        }

        public List<NhanVien> SearchByKeyword(string keyword)
        {
            keyword = keyword.ToLower();
            return _nhanViens
                .Where(x =>
                    x.MaNhanVien.ToLower().Contains(keyword) ||
                    x.Ten.ToLower().Contains(keyword))
                .ToList();
        }

        public bool CheckEmailExists(string email)
        {
            return _nhanViens.Any(x => x.Email == email);
        }

        public List<NhanVien> GetAll()
        {
            return _nhanViens;
        }
    }
}

