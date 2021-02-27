using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Interfaces;

namespace Tests_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public ITestService Service { get; set; }
        public TestController(ITestService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            return new JsonResult(await Service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] Tests_CRUD_BLL.Models.Test test)
        {
            await this.Service.CreateAsync(test);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTest(Tests_CRUD_BLL.Models.Test test)
        {
            return Ok(await this.Service.UpdateAsync(test));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestById(Guid id)
        {
            await this.Service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(Guid id)
        {
            var allTests = await this.Service.GetAllAsync();

            if (allTests.Any(x => x.Id == id))
            {
                return new JsonResult(allTests.First(x => x.Id == id));
            }

            return NotFound();
        }
    }
}
