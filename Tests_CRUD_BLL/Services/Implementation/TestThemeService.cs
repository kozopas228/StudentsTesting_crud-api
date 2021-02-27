using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Models;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class TestThemeService : ITestThemeService
    {
        public ITestThemeRepository Repository { get; set; }

        public TestThemeService(ITestThemeRepository repository)
        {
            this.Repository = repository;
        }
        public async Task<IEnumerable<TestTheme>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TestTheme obj)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TestTheme obj)
        {
            throw new NotImplementedException();
        }
    }
}