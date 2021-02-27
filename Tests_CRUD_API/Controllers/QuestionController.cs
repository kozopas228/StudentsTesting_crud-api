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
    public class QuestionController : ControllerBase
    {
        public IQuestionService Service { get; set; }
        public QuestionController(IQuestionService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            return new JsonResult(await Service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] Tests_CRUD_BLL.Models.Question question)
        {
            await this.Service.CreateAsync(question);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(Tests_CRUD_BLL.Models.Question question)
        {
            return Ok(await this.Service.UpdateAsync(question));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionById(Guid id)
        {
            await this.Service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var allQuestions = await this.Service.GetAllAsync();

            if (allQuestions.Any(x => x.Id == id))
            {
                return new JsonResult(allQuestions.First(x => x.Id == id));
            }

            return NotFound();
        }
    }
}
