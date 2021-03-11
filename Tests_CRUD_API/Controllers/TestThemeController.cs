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
    public class TestThemeController : ControllerBase
    {
        public ITestThemeService Service { get; set; }
        public TestThemeController(ITestThemeService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTestThemes()
        {
            return new JsonResult(await Service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestTheme([FromBody] Tests_CRUD_BLL.Models.TestTheme testTheme)
        {
            return Ok(await this.Service.CreateAsync(testTheme));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestTheme(Tests_CRUD_BLL.Models.TestTheme testTheme)
        {
            return Ok(await this.Service.UpdateAsync(testTheme));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestThemeById(Guid id)
        {
            await this.Service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestThemeById(Guid id)
        {
            var allTestThemes = await this.Service.GetAllAsync();

            if (allTestThemes.Any(x => x.Id == id))
            {
                return new JsonResult(allTestThemes.First(x => x.Id == id));
            }

            return NotFound();
        }
    }
}
