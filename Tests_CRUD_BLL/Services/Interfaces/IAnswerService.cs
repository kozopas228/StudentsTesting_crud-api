using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface IAnswerService : IGenericService<Models.Answer>
    {
        public IAnswerRepository Repository { get; set; }
        public IAnswerMapper Mapper { get; set; }
    }
}