using APIDBProject.Service;

using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;

namespace APIDBProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsApiController : ControllerBase
    {
        private readonly IStoreRepository<Product> _storeRepository;
        
        public ProductsApiController(IStoreRepository<Product> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _storeRepository.GetAllAsync());


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            if (await _storeRepository.Get(id) != null) {
                return Ok(await _storeRepository.Get(id));
            }
            return NotFound();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post(Product product) {
            try
            {
                await _storeRepository.Create(product);
                return Ok();
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Product product)
        {
            try{
               await _storeRepository.Update(product);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }



        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try
            {
                await _storeRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex) {
                return Conflict();
            }
            } 

    }
}
