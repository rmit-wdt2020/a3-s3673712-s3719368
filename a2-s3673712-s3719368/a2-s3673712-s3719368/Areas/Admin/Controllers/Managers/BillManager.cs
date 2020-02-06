
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
        public async Task<BillPayDto> GetBillPay(int? BillPayId)
        {
            if (BillPayId == null)
                return null;

            var response = await client.GetAsync($"api/BillPays/{BillPayId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var billpay = JsonConvert.DeserializeObject<BillPayDto>(result);

            return billpay;
        }
        public bool Block(BillPayDto bill, bool status)
        {

            bill.Block = status;
            bill.ModifyDate = DateTime.UtcNow;

            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json"); //encoding the obj
            var response = client.PutAsync("api/billpays", content).Result;
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
        public bool Unblock(BillPayDto bill, bool status)
        {

            bill.Block = status;
            bill.ModifyDate = DateTime.UtcNow;

            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json"); //encoding the obj
            var response = client.PutAsync("api/billpays", content).Result;
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
