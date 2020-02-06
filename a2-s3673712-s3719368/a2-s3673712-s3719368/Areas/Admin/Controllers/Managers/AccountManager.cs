using a2_s3673712_s3719368.Areas.Admin.Models;
using a2_s3673712_s3719368.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers.Managers
{
    public class AccountManager
    {
        private HttpClient client;
        public AccountManager()
        {
            client = WebApi.InitializeClient();
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var response = await client.GetAsync("api/Accounts");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            // Deserializing the response recieved from web api and storing into a list.
            IEnumerable<AccountDto> accounts = JsonConvert.DeserializeObject<List<AccountDto>>(result);

            return accounts;
        }

        public async Task<AccountDto> GetAccount(int? id)
        {
            if (id == null)
                return null;

            var response = await client.GetAsync($"api/Accounts/{id}");
            if (!response.IsSuccessStatusCode)
                throw new ItemNotFoundExcpetion("Status Failed");

            var result = response.Content.ReadAsStringAsync().Result;
            var account = JsonConvert.DeserializeObject<AccountDto>(result);

            return account;
        }
    }
}
