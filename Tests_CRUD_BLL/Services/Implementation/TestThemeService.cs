using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Implementation
{
    public class TestThemeService : ITestThemeService
    {
        public ITestThemeRepository Repository { get; set; }
        public ITestThemeMapper Mapper { get; set; }
        public TestThemeService(ITestThemeRepository repository, ITestThemeMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }
        public async Task<IEnumerable<Models.TestTheme>> GetAllAsync()
        {
            var testThemes = new List<Models.TestTheme>();

            var entities = await this.Repository.GetAllAsync();

            foreach (var testTheme in entities)
            {
                testThemes.Add(this.Mapper.ToDto(testTheme));
            }

            return testThemes;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await this.Repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Models.TestTheme obj)
        {
            var testTheme = await this.Mapper.ToEntityAsync(obj);

            return await this.Repository.UpdateAsync(testTheme);
        }

        public async Task<Guid> CreateAsync(Models.TestTheme obj)
        {
            var testTheme = await this.Mapper.ToEntityAsync(obj);

            return await this.Repository.CreateAsync(testTheme);
        }
    }
}