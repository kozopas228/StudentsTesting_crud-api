using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class AnswerService : IAnswerService
    {
        public IAnswerRepository Repository { get; set; }
        public IAnswerMapper Mapper { get; set; }
        public AnswerService(IAnswerRepository repository, IAnswerMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }
        public async Task<IEnumerable<Models.Answer>> GetAllAsync()
        {
            var answers = new List<Models.Answer>();

            var entities = await this.Repository.GetAllAsync();

            foreach (var answer in entities)
            {
                answers.Add(this.Mapper.ToDto(answer));
            }

            return answers;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await this.Repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Models.Answer obj)
        {
            var answer = await this.Mapper.ToEntityAsync(obj);

            return await this.Repository.UpdateAsync(answer);
        }

        public async Task<Guid> CreateAsync(Models.Answer obj)
        {
            var answer = await this.Mapper.ToEntityAsync(obj);

            var result = await this.Repository.CreateAsync(answer);

            return result;
        }

    }
}