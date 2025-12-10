using DAL_Xuong;
using DTO_Xuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_Xuong
{
    public class BUSMuonTraSach
    {
        DALMuonTraSach dalMuonTraSach = new DALMuonTraSach();
        DALSach dalSach = new DALSach();
        DALTrangThaiThanhToan dalThanhToan = new DALTrangThaiThanhToan();
        public List<MuonTra> GetAllMuonTra()
        {
            return dalMuonTraSach.selectAll();
        }
        public List<Sach> LoadBangSach()
        {
            return dalSach.LayDSSach(); // hoặc lọc sách đang áp dụng nếu cần
        }
        public List<MuonTra> GetMuonTraById(string keyword)
        {
            return dalMuonTraSach.selectById(keyword);
        }
        public List<TrangThaiThanhToan> LoadBangThanhToan()
        {
            return dalThanhToan.GetAllTrangThai(); // Đảm bảo DAL có hàm này
        }
        private DALMuonTraSach dal = new DALMuonTraSach();

        public string GenerateMuonTra()
        {
            return dal.GenerateMuonTra();
        }
        public string InsertRange(List<ChiTietMuonTra> lstChiTiet, string maMuonTra)
        {
            try
            {
                if (lstChiTiet == null || lstChiTiet.Count == 0)
                    return "Danh sách chi tiết không hợp lệ.";

                BUSChiTietMuonTra busCT = new BUSChiTietMuonTra();

                // 1. Lấy danh sách chi tiết hiện có trong DB để so sánh
                List<ChiTietMuonTra> chiTietCu = busCT.GetChiTietByMaMuonTra(maMuonTra);

                List<ChiTietMuonTra> insertList = new List<ChiTietMuonTra>();
                List<ChiTietMuonTra> updateList = new List<ChiTietMuonTra>();
                List<string> deleteList = new List<string>();

                // 2. Xác định bản ghi mới (insert) hoặc đã tồn tại (update)
                foreach (var ct in lstChiTiet)
                {
                    var ctCu = chiTietCu.FirstOrDefault(c => c.MaChiTiet == ct.MaChiTiet);
                    if (ctCu == null)
                    {
                        // Bản ghi mới
                        if (string.IsNullOrEmpty(ct.MaChiTiet))
                            ct.MaChiTiet = busCT.GenerateMaChiTiet();

                        ct.MaMuonTra = maMuonTra;
                        insertList.Add(ct);
                    }
                    else
                    {
                        // Bản ghi đã tồn tại → update
                        updateList.Add(ct);
                    }
                }

                // 3. Xác định bản ghi bị xóa
                foreach (var ctCu in chiTietCu)
                {
                    if (!lstChiTiet.Any(c => c.MaChiTiet == ctCu.MaChiTiet))
                    {
                        deleteList.Add(ctCu.MaChiTiet);
                    }
                }

                // 4. Thực hiện lưu dữ liệu
                if (insertList.Count > 0)
                {
                    string result = busCT.InsertRange(insertList, maMuonTra);
                    if (!string.IsNullOrEmpty(result)) return result;
                }

                if (updateList.Count > 0)
                {
                    foreach (var ct in updateList)
                    {
                        string result = busCT.UpdateChiTietMuon(ct);
                        if (!string.IsNullOrEmpty(result)) return result;
                    }
                }

                if (deleteList.Count > 0)
                {
                    string result = busCT.DeleteRange(deleteList);
                    if (!string.IsNullOrEmpty(result)) return result;
                }

                return string.Empty; // thành công
            }
            catch (Exception ex)
            {
                return "Lỗi khi lưu danh sách chi tiết: " + ex.Message;
            }
        }

        public string InsertMuonTra(MuonTra s, List<ChiTietMuonTra> listCT)
        {
            try
            {
                s.MaMuonTra = dalMuonTraSach.generateMuonTra();
                if (string.IsNullOrEmpty(s.MaMuonTra))
                {
                    return "Mã không hợp lệ.";
                }

                dalMuonTraSach.insertMuonTra(s);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm mượn trả sách: " + ex.Message;
            }
        }

        public string InsertMuonTraReturn(MuonTra s)
        {
            try
            {
                s.MaMuonTra = dalMuonTraSach.generateMuonTra();
                if (string.IsNullOrEmpty(s.MaMuonTra))
                {
                    return "Mã không hợp lệ.";
                }

                return dalMuonTraSach.insertMuonTraReturn(s);
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm mượn trả sách: " + ex.Message;
            }
        }

        public string UpdateMuonTra(MuonTra s)
        {
            try
            {
                if (string.IsNullOrEmpty(s.MaMuonTra))
                {
                    return "Mã  không hợp lệ.";
                }

                dalMuonTraSach.updateMuonTra(s);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi cập nhật : " + ex.Message;
            }
        }

        public string DeleteMuonTra(string maMuonTra)
        {
            try
            {
                if (string.IsNullOrEmpty(maMuonTra))
                {
                    return "Mã không hợp lệ.";
                }

                dalMuonTraSach.deleteMuonTra(maMuonTra);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa phí sách: " + ex.Message;
            }
        }
    }
}
