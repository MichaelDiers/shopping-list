// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingList.BaseItems.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShoppingList.BaseItems.Contracts;
    using ShoppingList.BaseItems.Models;
    using ShoppingList.BaseItems.Requests;

    [Route("api/[controller]")]
    [ApiController]
    public class BaseItemController : ControllerBase
    {
        private readonly IBaseItemProvider provider;

        public BaseItemController(IBaseItemProvider provider)
        {
            this.provider = provider;
        }

        // DELETE api/<BaseItemController>/58a7e6b8-03c9-4546-9a1e-d93a3d34202c
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.provider.Delete(id);
            return this.NoContent();
        }

        // GET: api/<BaseItemController>
        [HttpGet]
        public async Task<IEnumerable<BaseItem>> Get()
        {
            return await this.provider.Read();
        }

        // GET api/<BaseItemController>/58a7e6b8-03c9-4546-9a1e-d93a3d34202c
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var baseItem = await this.provider.Read(id);
            return this.Ok(baseItem);
        }

        // POST api/<BaseItemController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRequest request)
        {
            var result = await this.provider.Create(request);
            return this.Created(
                new Uri(
                    $"{this.Request.Path}/{result.Id}",
                    UriKind.Relative),
                result);
        }

        // PUT api/<BaseItemController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BaseItem baseItem)
        {
            await this.provider.Update(baseItem);
            return this.NoContent();
        }
    }
}
