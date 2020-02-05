using a2_s3673712_s3719368.Areas.Admin.Models;
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

        public async Task<IEnumerable<TransactionDto>> GetTransactionAsync(int? id) 
        {
            if (id == null)
                return null;
            var response = await client.GetAsync($"api/Transactions/{id}");

            return null;

        }
    }
}
