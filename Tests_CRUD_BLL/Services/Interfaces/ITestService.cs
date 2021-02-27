using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface ITestService : IGenericService<Models.Test>
    {
        public ITestRepository Repository { get; set; }
        public ITestMapper Mapper { get; set; }
    }
}