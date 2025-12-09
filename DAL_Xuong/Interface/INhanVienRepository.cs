using DTO_Xuong;

namespace DAL_Xuong.Interfaces    // <- phải có .Interfaces
{
    public interface INhanVienRepository
    {
        void ThemNhanVien(NhanVien nv);
        void SuaNhanVien(NhanVien nv);
        void XoaNhanVien(string maNV);
        NhanVien TimNhanVienTheoMa(string maNV);
        List<NhanVien> LayTatCa();
    }
}