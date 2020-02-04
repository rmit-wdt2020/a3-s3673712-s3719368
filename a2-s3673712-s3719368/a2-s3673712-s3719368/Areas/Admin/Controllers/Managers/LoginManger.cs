using a2_s3673712_s3719368.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers.Managers
{
    public class LoginManger
    {
        private HttpClient client;
        public LoginManger()
        {
            client = WebApi.InitializeClient();
        }

        public async Task<LoginDto> GetLogin(int? id)
        {
            if (id == null)
                return null;

            var response = await client.GetAsync($"api/Logins/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var login = JsonConvert.DeserializeObject<LoginDto>(result);

            return login;
        }

        public bool Lock(LoginDto login,bool status)
        {

            login.Lock = status;
            if(status == true)
             login.LockDate = DateTime.UtcNow;

            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"); //encoding the obj
            var response = client.PutAsync("api/Logins", content).Result;
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
