using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Areas.Admin.Models;
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

        public async Task<IEnumerable<TransactionDto>> GetTransaction(int? id) 
        {
            if (id == null)
                return null;
            var response = await client.GetAsync($"api/Accounts/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            AccountDto account = JsonConvert.DeserializeObject<AccountDto>(result);
            IEnumerable<TransactionDto> transactions = account.Transactions;

            return transactions;

        }

        public IEnumerable<TransactionDto> FliterByDate(IEnumerable<TransactionDto> transactions, DateTime FromDate, DateTime ToDate) 
        {
            List<TransactionDto> transactionArray = transactions.ToList();
            for (int i = 0; i < transactionArray.Count(); i++) 
            {
                if (transactionArray[i].TransactionTimeUtc < FromDate.ToUniversalTime() || transactionArray[i].TransactionTimeUtc > ToDate.ToUniversalTime())
                {
                    transactionArray.Remove(transactionArray[i]);
                }
            }

            return transactionArray;
        }
    }
}
