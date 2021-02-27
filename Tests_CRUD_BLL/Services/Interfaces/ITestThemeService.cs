using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface ITestThemeService : IGenericService<Models.TestTheme>
    {
        public ITestThemeRepository Repository { get; set; }
        public ITestThemeMapper Mapper { get; set; }
    }
}