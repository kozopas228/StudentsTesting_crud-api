using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface IQuestionService : IGenericService<Models.Question>
    {
        public IQuestionRepository Repository { get; set; }
        public IQuestionMapper Mapper { get; set; }
    }
}