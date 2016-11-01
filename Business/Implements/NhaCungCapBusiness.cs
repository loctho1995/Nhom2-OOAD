using Common.Models;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business.Implements
{
    public class NhaCungCapBusiness
    {
        private SMSEntities _dbContext;
        private readonly NhaCungCapRepository _nhaCungCapRepo;

        public NhaCungCapBusiness()
        {
            _dbContext = new SMSEntities();
            _nhaCungCapRepo = new NhaCungCapRepository(_dbContext);
        }

        public List<Object> LoadNhaCungCap()
        {
            IQueryable<NhaCungCap> dsChucVu = _nhaCungCapRepo.GetAll();

            var list = (from chucvu in dsChucVu
                        select new SelectListItem
                        {
                            Text = chucvu.TenNhaCungCap,
                            Value = chucvu.MaNhaCungCap.ToString(),
                        });
            return new List<Object>(list);
        }
    }
}
