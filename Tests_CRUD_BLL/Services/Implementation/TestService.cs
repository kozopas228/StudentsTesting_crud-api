using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class TestService : ITestService
    {
        public ITestRepository Repository { get; set; }
        public ITestMapper Mapper { get; set; }
        public TestService(ITestRepository repository, ITestMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }
        public async Task<IEnumerable<Models.Test>> GetAllAsync()
        {
            var tests = new List<Models.Test>();

            var entities = await this.Repository.GetAllAsync();

            foreach (var test in entities)
            {
                tests.Add(this.Mapper.ToDto(test));
            }

            return tests;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await this.Repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Models.Test obj)
        {
            var test = await this.Mapper.ToEntityAsync(obj);

            return await this.Repository.UpdateAsync(test);
        }

        public async Task CreateAsync(Models.Test obj)
        {
            var test = await this.Mapper.ToEntityAsync(obj);

            await this.Repository.CreateAsync(test);
        }
    }
}