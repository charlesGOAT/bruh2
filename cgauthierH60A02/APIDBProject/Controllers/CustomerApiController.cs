using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using System.Security.Cryptography.X509Certificates;

namespace APIDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerApiController : ControllerBase
    {
        private readonly IStoreRepository<Customer> _storeRepository;

        public CustomerApiController(IStoreRepository<Customer> storeRepository)
        {
            _storeRepository = storeRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Customer customer) {
            try
            {
                await _storeRepository.Create(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {

            try
            {
                var customers = await _storeRepository.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/[controller]/{id}")]
        public async Task<IActionResult> Get(int id) { 
        
        return Ok(_storeRepository.Get(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer) {
            try
            {
                await _storeRepository.Update(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("api/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id) {

            try
            {
                await _storeRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
