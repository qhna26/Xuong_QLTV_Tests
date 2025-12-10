using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Xuong;

namespace DAL_Xuong   // ←←←← ĐÚNG RỒNG RẮN Ở ĐÂY (không có .Interface)
{
    public interface INhanVienRepository
    {
        bool Add(NhanVien nv);
        bool Delete(string maNhanVien);
        bool Update(NhanVien nv);
        NhanVien GetById(string maNhanVien);
        List<NhanVien> SearchByKeyword(string keyword);
        bool CheckEmailExists(string email);
        List<NhanVien> GetAll();

    }
}