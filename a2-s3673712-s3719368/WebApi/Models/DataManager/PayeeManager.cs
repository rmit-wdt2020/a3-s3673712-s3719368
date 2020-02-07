
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

        public Payee Get(int id) //get Payee with specific id
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<Payee> GetAll() //get all payees include billpay
        {
            return _context.Payees.Include(b => b.BillPay).ToList();
        }

        public int Add(Payee payee) //insert Payee obj into datbase
        {
            _context.Payees.Add(payee);
            _context.SaveChanges();

            return payee.PayeeID;
        }

        public int Delete(int id) //delete Payee by Id
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Payee payee) //Update the payee in datatbase with specific id
        {
            _context.Update(payee);
            _context.SaveChanges();

            return id;
        }
    }
}
