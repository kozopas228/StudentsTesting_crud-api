using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Models;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class TestService : ITestService
    {
        public ITestRepository Repository { get; set; }

        public TestService(ITestRepository repository)
        {
            this.Repository = repository;
        }
        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Test obj)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Test obj)
        {
            throw new NotImplementedException();
        }
    }
}