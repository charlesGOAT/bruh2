using APIDBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace APIDBProject.Service
{
    public class CustomerService : ControllerBase, IStoreRepository<Customer>
    {
       
        private readonly StoreContext _context;
      
        public CustomerService(StoreContext context)
        {
            _context = context;
        }

        public async Task Create(Customer customer)
        {
           await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public HttpClient CreateClient()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.First(x => x.CustomerId == id));
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> Get(int? id)
        {
            return await _context.Customers.FirstAsync(m => m.CustomerId == id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public Task<Dictionary<int, string>> GetImages(List<Customer> t)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveImage(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
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
