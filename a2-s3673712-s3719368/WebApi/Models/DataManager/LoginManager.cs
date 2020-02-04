

using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Models.DataManager
{
    public class LoginManager : IDataRepository<Login, int>
    {
        private readonly NationBankContext _context;

        public LoginManager(NationBankContext context)
        {
            _context = context;
        }

        public Login Get(int id)//get by customerID
        {
            return _context.Logins.Where(e => e.CustomerID == id).FirstOrDefault();
        }

        public IEnumerable<Login> GetAll()
        {
            return _context.Logins.Include(c => c.Customer).ThenInclude(a => a.Accounts).ToList();
        }

        public int Add(Login login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return Convert.ToInt32(login.LoginID);
        }

        public int Delete(int id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Login login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }
    }
}
