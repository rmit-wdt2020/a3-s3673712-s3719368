using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<TransactionDto>> GetAllTransaction() 
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

        public async Task<IEnumerable<TransactionDto>> GetTransaction(int? id) 
        {
            if (id == null)
                return null;
            var response = await client.GetAsync($"api/Accounts/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ItemNotFoundExcpetion("Status Failed");

            var result = response.Content.ReadAsStringAsync().Result;
            AccountDto account = JsonConvert.DeserializeObject<AccountDto>(result);
            IEnumerable<TransactionDto> transactions = account.Transactions;

            return transactions;

        }

        public IEnumerable<TransactionDto> FliterByDate(IEnumerable<TransactionDto> transactions, DateTime FromDate, DateTime ToDate) 
        {
            List<TransactionDto> transactionArray = transactions.ToList();
            foreach(TransactionDto item in transactions)
            {
                if (item.TransactionTimeUtc < FromDate.ToUniversalTime() || item.TransactionTimeUtc > ToDate.ToUniversalTime()) 
                {
                    transactionArray.Remove(item);
                }
            }

            return transactionArray;
        }
    }
}
