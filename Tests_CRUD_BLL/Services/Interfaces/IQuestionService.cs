using Tests_CRUD_BLL.Models;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface IQuestionService:IGenericService<Question>
    {
        public IQuestionRepository Repository { get; set; }
    }
}