
using a2_s3673712_s3719368.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers.Managers
{
    public class BillManager
    {
        private HttpClient client;
        public BillManager()
        {
            client = WebApi.InitializeClient();
        }
        public async Task<IEnumerable<BillPayDto>> GetAllBillPays()
        {
            var response = await client.GetAsync("api/BillPays");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            // Deserializing the response recieved from web api and storing into a list.
            IEnumerable<BillPayDto> billpays = JsonConvert.DeserializeObject<List<BillPayDto>>(result);

            return billpays;
        }
    }
}
