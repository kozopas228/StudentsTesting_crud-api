using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        public IQuestionRepository Repository { get; set; }
        public IQuestionMapper Mapper { get; set; }
        public QuestionService(IQuestionRepository repository, IQuestionMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }
        public async Task<IEnumerable<Models.Question>> GetAllAsync()
        {
            var questions = new List<Models.Question>();

            var entities = await this.Repository.GetAllAsync();

            foreach (var question in entities)
            {
                questions.Add(this.Mapper.ToDto(question));
            }

            return questions;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await this.Repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Models.Question obj)
        {
            var question = await this.Mapper.ToEntityAsync(obj);

            return await this.Repository.UpdateAsync(question);
        }

        public async Task CreateAsync(Models.Question obj)
        {
            var question = await this.Mapper.ToEntityAsync(obj);

            await this.Repository.CreateAsync(question);
        }
    }
}