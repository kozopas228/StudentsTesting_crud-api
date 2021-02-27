using Tests_CRUD_BLL.Models;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface ITestThemeService : IGenericService<TestTheme>
    {
        public ITestThemeRepository Repository { get; set; }
    }
}