using Tests_CRUD_BLL.Models;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Services.Interfaces
{
    public interface IAnswerService:IGenericService<Answer>
    {
        public IAnswerRepository Repository { get; set; }
    }
}