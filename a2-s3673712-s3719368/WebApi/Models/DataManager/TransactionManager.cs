
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
    public class TransactionManager : IDataRepository<Transaction, int>
    {
        private readonly NationBankContext _context;

        public TransactionManager(NationBankContext context)
        {
            _context = context;
        }

        public Transaction Get(int id)//get by accountnumber
        {
            return _context.Transactions.Where(e => e.AccountNumber == id).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.Include(e => e.DestinationAccount).ThenInclude(e => e.AccountNumber).ToList();
        }

        public int Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionID;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }
    }
}
