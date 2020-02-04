
using a2_s3673712_s3719368.Models;
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Models.DataManager
{
    public class PayeeManager : IDataRepository<Payee, int>
    {
        private readonly NationBankContext _context;

        public PayeeManager(NationBankContext context)
        {
            _context = context;
        }

        public Payee Get(int id)
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<Payee> GetAll()
        {
            return _context.Payees.Include(b => b.BillPay).ToList();
        }

        public int Add(Payee payee)
        {
            _context.Payees.Add(payee);
            _context.SaveChanges();

            return payee.PayeeID;
        }

        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Payee payee)
        {
            _context.Update(payee);
            _context.SaveChanges();

            return id;
        }
    }
}
