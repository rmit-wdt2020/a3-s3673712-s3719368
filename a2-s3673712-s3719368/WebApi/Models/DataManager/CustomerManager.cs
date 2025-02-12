﻿
using a2_s3673712_s3719368.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace BankAPI.Models.DataManager
{
    public class CustomerManager : IDataRepository<Customer, int>
    {
        private readonly NationBankContext _context;

        public CustomerManager(NationBankContext context)
        {
            _context = context;
        }

        public Customer Get(int id) //get Customer with specific id
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAll() //get all Customer include accounts
        {
            return _context.Customers.Include( c => c.Accounts).ToList();
        }

        public int Add(Customer customer) //insert customer obj into datbase
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.CustomerID;
        }

        public int Delete(int id) //delete Customer by Id
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Customer customer) //Update the Customer in datatbase with specific id
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }
    }
}
