using System.Collections.Generic;
using ExampleAspNetProject.Models;
using ExampleAspNetProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAspNetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesDataService _valuesDataService;

        public ValuesController(IValuesDataService valuesDataService)
        {
            _valuesDataService = valuesDataService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ValueModel>> Get()
        {
            return Ok(_valuesDataService.Read());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ValueModel> Get(int id)
        {
            return _valuesDataService.Read(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var model = _valuesDataService.Create(value);
            Created(model.Id.ToString(), model);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _valuesDataService.Update(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _valuesDataService.Delete(id);
        }
    }
}
