using APIDBProject.Service;

using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;

namespace APIDBProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductsCategoryApiController : ControllerBase
    {
        private readonly IStoreRepository<ProductCategory> _storeRepository;
        public ProductsCategoryApiController(IStoreRepository<ProductCategory> storeRepository)
        {
            _storeRepository = storeRepository;

        }

        [HttpGet]
        public async Task<List<ProductCategory>> Get() => await _storeRepository.GetAllAsync();



        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            if (await _storeRepository.Get(id) != null)
            {
                return Ok(await _storeRepository.Get(id));
            }
            return NotFound();

        }

        [HttpPost]
        public async Task <IActionResult> Post(ProductCategory category)
        {
            try
            {
                await _storeRepository.Create(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ProductCategory category)
        {
            try
            {
                await _storeRepository.Update(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _storeRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
           
        }
    }
}

