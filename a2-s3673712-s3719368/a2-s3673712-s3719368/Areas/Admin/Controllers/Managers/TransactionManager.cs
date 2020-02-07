using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Exceptions;
using a2_s3673712_s3719368.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers.Managers
{
    public class TransactionManager
    {
        private HttpClient client;
        public TransactionManager()
        {
            client = WebApi.InitializeClient();
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransaction() //get all transactions from web api using the httpclient
        {
            var response = await client.GetAsync("api/Transactions");
            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var tr = JsonConvert.DeserializeObject<List<TransactionDto>>(result);

            return tr;

        }

        public async Task<IEnumerable<TransactionDto>> GetTransaction(int? id)  //get transaction with spefic id from web api
        {
            if (id == null)
                return null;
            var response = await client.GetAsync($"api/Accounts/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ItemNotFoundExcpetion("Status Failed");

            var result = response.Content.ReadAsStringAsync().Result;
            AccountDto account = JsonConvert.DeserializeObject<AccountDto>(result);
            IEnumerable<TransactionDto> transactions = account.Transactions.OrderBy(e => e.TransactionTimeUtc);

            return transactions;

        }

        public IEnumerable<TransactionDto> FliterByDate(IEnumerable<TransactionDto> transactions, DateTime FromDate, DateTime ToDate) //fliter transaction by date, only returns transaction  in dates
        {
            List<TransactionDto> transactionArray = transactions.ToList();
            if (transactionArray[0].TransactionTimeUtc > FromDate)
                FromDate = transactionArray[0].TransactionTimeUtc;
            foreach (TransactionDto item in transactions)
            {
                if (item.TransactionTimeUtc < FromDate.ToUniversalTime() || item.TransactionTimeUtc > ToDate.ToUniversalTime()) 
                {
                    transactionArray.Remove(item);
                }
            }

            return transactionArray;
        }

        public string GetTimePeriodOfTransaction(DateTime FromDate, DateTime ToDate) //return string of months between 2 dates
        {
            string period = "";

            for (int i = FromDate.Month; i <= ToDate.Month; i++) 
            {
                period += "'" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).ToString() + "'" + ",";
            }

            return period;
        }

        public string GetDataBetweenMonth(IEnumerable<TransactionDto> transactions, DateTime FromDate, DateTime ToDate) //get transaction total money data between 2 months
        {
            string data = "";
            List<TransactionDto> transactionArray = transactions.ToList();
            List<int> Months = new List<int>();
            for (int i = FromDate.Month; i <= ToDate.Month; i++)
            {
                Months.Add(i); //add month as group
            }


            foreach (int month in Months) 
            {
                data += GetMonthsTotal(month, transactions).ToString() + ",";
            }
           
            return data;
        }

        public string GetFrequnceyBetweenMonth(IEnumerable<TransactionDto> transactions, DateTime FromDate, DateTime ToDate) //get transaction frequency data between 2 months
        {
            string data = "";
            List<TransactionDto> transactionArray = transactions.ToList();
            List<int> Months = new List<int>();
            for (int i = FromDate.Month; i <= ToDate.Month; i++)
            {
                Months.Add(i); //add month as group
            }
            foreach (int month in Months)
            {
                data += GetFrequnceyMonthTotal(month, transactions).ToString() + ",";
            }
            return data;
        }

        public decimal GetMonthsTotal(int Month, IEnumerable<TransactionDto> transactions)  //get the total money of transactions in a month
        {
            decimal total = 0;
            foreach (TransactionDto item in transactions)
            {
                if (item.TransactionTimeUtc.Month == Month) 
                {
                    total += item.Amount;
                }
            }

            return total;
        }

        public int GetFrequnceyMonthTotal(int Month, IEnumerable<TransactionDto> transactions)  //get the transaction times of transactions in a month
        {
            int total = 0;
            foreach (TransactionDto item in transactions)
            {
                if (item.TransactionTimeUtc.Month == Month)
                {
                    total ++;
                }
            }

            return total;
        }

        public string GetTrasnactionTypeList() //return all type of transaction type
        {
            string list = "";
            foreach (string typeString in Enum.GetNames(typeof(TransactionType))) 
            {
                list += "'" + typeString + "',";
            }
            return list;
        }

        public string GetTransactionTypeData(IEnumerable<TransactionDto> transactions) //get count of transaction types amoung a group of transactions
        {
            string result = "";
            foreach (string typeString in Enum.GetNames(typeof(TransactionType)))
            {
                result += GetTransactionTypeCount(typeString,transactions) +",";
            }
            return result;
        }

        public int GetTransactionTypeCount(string type, IEnumerable<TransactionDto> transactions) //count the amount of  1 type of transaction in a group of transactions
        {
            int count = 0;
            foreach (TransactionDto transaction in transactions) 
            {
                if (transaction.TransactionType.ToString() == type) 
                {
                    count++;
                }
            }
            return count;
        }

    }
}
