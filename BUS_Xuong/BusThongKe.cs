using DTO_Xuong;
using DAL_Xuong;
using System.Collections.Generic;
using static DTO_Xuong.ThongKe;

namespace BUS_Xuong
{
    public class BusThongKe
    {
        private DALThongKe dalThongKe = new DALThongKe();

        // Thống kê tất cả nhân viên (không lọc ngày)
        public List<ThongKeDTO> LayThongKeTatCaNhanVien()
        {
            return dalThongKe.ThongKeMuonTheoNhanVien();
        }

        // Thống kê theo 1 nhân viên
        public ThongKeDTO ThongKeTheoMaNhanVien(string maNV)
        {
            return dalThongKe.ThongKeTheoMaNhanVien(maNV);
        }
        

        // Thống kê theo khoảng thời gian (tất cả nhân viên)
        public List<ThongKeDTO> LayThongKeTheoNhanVien(DateTime tuNgay, DateTime denNgay)
        {
            return dalThongKe.LayThongKeTheoNhanVien(tuNgay, denNgay);
        }
    }
}
