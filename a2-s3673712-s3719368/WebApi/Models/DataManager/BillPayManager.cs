
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Models.DataManager
{
    public class BillPayManager : IDataRepository<BillPay, int>
    {
        private readonly NationBankContext _context;

        public BillPayManager(NationBankContext context)
        {
            _context = context;
        }
     
        public BillPay Get(int id) //get Bill with specific id
        {
            return _context.BillPays.Where(b => b.BillPayID == id).FirstOrDefault();
        }

        public IEnumerable<BillPay> GetAll() //get all bills include account and payee
        {
            return _context.BillPays.Include(a => a.Account).Include(p => p.Payee).ToList();
        }

        public int Add(BillPay bill) //insert billpay obj into datbase
        {
            _context.BillPays.Add(bill);
            _context.SaveChanges();

            return bill.BillPayID;
        }

        public int Delete(int id) //delete bill by Id
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, BillPay bill)  //Update the BillPay in datatbase with specific id
        {
            _context.Update(bill);
            _context.SaveChanges();

            return id;
        }
    }
}
