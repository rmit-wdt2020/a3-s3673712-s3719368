
using a2_s3673712_s3719368.Models;
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace BankAPI.Models.DataManager
{
    public class CustomerManager : IDataRepository<Customers, int>
    {
        private readonly NationBankContext _context;

        public CustomerManager(NationBankContext context)
        {
            _context = context;
        }

        public Customers Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customers> GetAll()
        {
            return _context.Customers.Include( c => c.Accounts).ToList();
        }

        public int Add(Customers customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.CustomerId;
        }

        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Customers customer)
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }
    }
}
