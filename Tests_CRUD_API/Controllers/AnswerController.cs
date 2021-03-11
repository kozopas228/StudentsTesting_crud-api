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
    public class AnswerController : ControllerBase
    {
        public IAnswerService Service { get; set; }
        public AnswerController(IAnswerService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            return new JsonResult(await Service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody]Tests_CRUD_BLL.Models.Answer answer)
        {
            var result = await this.Service.CreateAsync(answer);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(Tests_CRUD_BLL.Models.Answer answer)
        {
            return Ok(await this.Service.UpdateAsync(answer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerById(Guid id)
        {
            await this.Service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(Guid id)
        {
            var allAnswers = await this.Service.GetAllAsync();

            if (allAnswers.Any(x => x.Id == id))
            {
                return new JsonResult(allAnswers.First(x=>x.Id==id));
            }

            return NotFound();
        }
    }
}
