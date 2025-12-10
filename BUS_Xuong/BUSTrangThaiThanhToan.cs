using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Xuong;
using DTO_Xuong;

namespace BUS_Xuong
{
    public class BUSTrangThaiThanhToan
    {
        private DALTrangThaiThanhToan dal = new DALTrangThaiThanhToan();

        public string GetNextMaSach()
        {
            return dal.GenerateNextMaSach();
        }

        public List<TrangThaiThanhToan> GetAll()
        {
            return dal.GetAllTrangThai(); 
        }

        public bool Insert(TrangThaiThanhToan t)
        {
            return dal.Insert(t); 
        }

        public bool Update(TrangThaiThanhToan t)
        {
            return dal.Update(t);
        }

        public bool Delete(string ma)
        {
            return dal.Delete(ma);
        }
    }

}
