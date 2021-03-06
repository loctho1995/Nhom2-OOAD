﻿using Common.Models;
using Data.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implements
{
    public class PhieuNhapKhoRepository : GenericRepository<PhieuNhap>
    {
        public PhieuNhapKhoRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public int CountPhieuNhapKho(PhieuNhapKhoRepository _phieuNhapKhoRepo)
        {
            return _phieuNhapKhoRepo.GetAll().Count();
        }
    }
}
