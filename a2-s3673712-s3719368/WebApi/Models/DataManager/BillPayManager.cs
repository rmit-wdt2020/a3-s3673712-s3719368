using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DataManager
{
    public class BillPayManager : IDataRepository<BillPays, int>
    {
        private readonly NationBankContext _context;

        public BillPayManager(NationBankContext context)
        {
            _context = context;
        }

        public BillPays Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        public IEnumerable<BillPays> GetAll()
        {
            return _context.BillPays.Include(a => a.AccountNumberNavigation).ToList();
        }

        public int Add(BillPays bill)
        {
            _context.BillPays.Add(bill);
            _context.SaveChanges();

            return bill.BillPayId;
        }

        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, BillPays bill)
        {
            _context.Update(bill);
            _context.SaveChanges();

            return id;
        }
    }
}
