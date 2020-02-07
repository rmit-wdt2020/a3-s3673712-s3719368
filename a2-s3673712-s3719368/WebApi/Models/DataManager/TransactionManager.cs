
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

        public Transaction Get(int id)//get Transaction by accountnumber
        {
            return _context.Transactions.Include(e => e.DestinationAccount).FirstOrDefault(e=> e.TransactionID == id);
        }

        public IEnumerable<Transaction> GetAll() //get all Transactions include billpay
        {
            return _context.Transactions.Include(e => e.DestinationAccount).ToList();
        }

        public int Add(Transaction transaction) //insert transaction obj into datbase
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionID;
        }

        public int Delete(int id) //delete Transaction by Id
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Transaction transaction) //Update the Transaction in datatbase with specific id
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }
    }
}
