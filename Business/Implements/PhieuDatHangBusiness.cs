using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class PhieuDatHangBusiness
    {
        private SMSEntities dbContext;

        public PhieuDatHangBusiness()
        {
            dbContext = new SMSEntities();          
        }

        public int Insert(PhieuDatHang order)
        {
            dbContext.PhieuDatHangs.Add(order);
            dbContext.SaveChanges();
            return order.SoPhieuDatHang;
        }

        public bool Update(PhieuDatHang entity)
        {
            try
            {
                var user = dbContext.PhieuDatHangs.Find(entity.SoPhieuDatHang);
                user.TongTien = entity.TongTien;

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
