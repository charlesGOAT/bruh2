using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class CustomerRepository : IStoreRepository<Customer>
    {
        public async Task Create(Customer customer)
        {
            var Client = CreateClient();
            await Client.PostAsJsonAsync("http://localhost:47733/api/CustomerApi", customer);
        }

        public HttpClient CreateClient()
        {
            HttpClient Client = new();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            return Client;
        }

        public async Task Delete(int id)
        {
            var Client = CreateClient();
            await Client.DeleteAsync($"http://localhost:47733/api/CustomerApi/{id}");
        }

        public async Task<Customer> Get(int? id)
        {
            var Client = CreateClient();
            var customer = await Client.GetFromJsonAsync<Customer>($"http://localhost:47733/api/CustomerApi/{id}");
            return customer;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var Client = CreateClient();
           var customers =  await Client.GetFromJsonAsync<List<Customer>>("http://localhost:47733/api/CustomerApi");
            return customers;
        }

        public Task<Dictionary<int, string>> GetImages(List<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveImage(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Customer customer)
        {
            var Client = CreateClient();
            await Client.PutAsJsonAsync("http://localhost:47733/api/CustomerApi", customer);
        }

        public Task UploadImage(Customer customer, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
