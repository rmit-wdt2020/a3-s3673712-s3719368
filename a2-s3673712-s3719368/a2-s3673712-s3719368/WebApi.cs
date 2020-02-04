using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368
{
    public class WebApi
    {
        private const string ApiBaseUri = "http://localhost:59505";
        public static HttpClient InitializeClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
