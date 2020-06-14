using myFacility.Payment.Core.Paystack.Interfaces;
using myFacility.Payment.Core.Paystack.Models.Customers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace myFacility.Payment.Core.Services.Customers
{
    public class PaystackCustomers : ICustomers
    {

        private string _secretKey;
        public PaystackCustomers(string secretKey)
        {
            this._secretKey = secretKey;
        }

      

        public async Task<CustomerCreationResponse> CreateCustomer(string email, string firstname = null, string lastname = null,string phone=null)
        {
            var client = HttpConnection.CreateClient(this._secretKey);

            var bodyKeyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("first_name", firstname),
                new KeyValuePair<string, string>("last_name", lastname),
                new KeyValuePair<string, string>("phone", phone)
            };

            var formContent = new FormUrlEncodedContent(bodyKeyValues);

            var response = await client.PostAsync("customer", formContent);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerCreationResponse>(json);
        }

        public async Task<CustomerCreationResponse> FetchCustomer(int id)
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync($"customer/{id}");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerCreationResponse>(json);
        }

        public async Task<CustomerListResponse> ListCustomers()
        {
            var client = HttpConnection.CreateClient(this._secretKey);
            var response = await client.GetAsync("customer");

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerListResponse>(json);
        }
    }
}
