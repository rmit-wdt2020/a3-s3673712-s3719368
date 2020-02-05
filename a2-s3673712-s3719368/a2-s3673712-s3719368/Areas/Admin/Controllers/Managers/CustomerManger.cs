using a2_s3673712_s3719368.Area.Admin.Models;
using a2_s3673712_s3719368.Exceptions;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Controllers.Managers
{
    public class CustomerManger
    {
        private HttpClient client;
        public CustomerManger() {
            client = WebApi.InitializeClient();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers() {
            var response = await client.GetAsync("api/Customers");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            // Deserializing the response recieved from web api and storing into a list.
            IEnumerable<CustomerDto> customers = JsonConvert.DeserializeObject<List<CustomerDto>>(result);

            return customers;
        }

        public async Task<CustomerDto> GetCustomer(int? id) 
        {
            if (id == null)
                return null;

            var response = await client.GetAsync($"api/Customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ItemNotFoundExcpetion("Status Failed");

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);

            return customer;
        }

        public bool Edit(CustomerDto customer)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"); //encoding the obj
            var response = client.PutAsync("api/Customers", content).Result;
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public bool delete(int id) 
        {
            var response = client.DeleteAsync($"api/Customers/{id}").Result;
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
