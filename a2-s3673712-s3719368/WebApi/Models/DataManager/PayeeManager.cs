using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.DataManager
{
    public class PayeeManager : IDataRepository<Payees, int>
    {
        private readonly NationBankContext _context;

        public PayeeManager(NationBankContext context)
        {
            _context = context;
        }

        public Payees Get(int id)
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<Payees> GetAll()
        {
            return _context.Payees.Include(b => b.BillPays).ToList();
        }

        public int Add(Payees payee)
        {
            _context.Payees.Add(payee);
            _context.SaveChanges();

            return payee.PayeeId;
        }

        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Payees payee)
        {
            _context.Update(payee);
            _context.SaveChanges();

            return id;
        }
    }
}
