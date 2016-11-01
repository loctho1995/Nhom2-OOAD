using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class ChiTietPhieuDatHangBusiness
    {
        SMSEntities dbContext;
        public ChiTietPhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();
        }
        public bool Insert(ChiTietPhieuDatHang detail)
        {
            try
            {
                dbContext.ChiTietPhieuDatHangs.Add(detail);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
