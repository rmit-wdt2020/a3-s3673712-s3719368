
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
    public class LoginManager : IDataRepository<Logins, int>
    {
        private readonly NationBankContext _context;

        public LoginManager(NationBankContext context)
        {
            _context = context;
        }

        public Logins Get(int id)
        {
            return _context.Logins.Find(id);
        }

        public IEnumerable<Logins> GetAll()
        {
            return _context.Logins.Include(c => c.Customer).ThenInclude(a => a.Accounts).ToList();
        }

        public int Add(Logins login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return Convert.ToInt32(login.LoginId);
        }

        public int Delete(int id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Logins login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }
    }
}
