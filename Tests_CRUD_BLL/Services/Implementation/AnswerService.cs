using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Models;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class AnswerService : IAnswerService
    {
        public IAnswerRepository Repository { get; set; }
        public AnswerService(IAnswerRepository repository)
        {
            this.Repository = repository;
        }
        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Answer obj)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Answer obj)
        {
            throw new NotImplementedException();
        }

    }
}