using System.Threading.Tasks;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers.Interfaces
{
    public interface IGenericMapper<TDto, TEntity>
    {
        ITestRepository TestRepository { get; set; }
        IAnswerRepository AnswerRepository { get; set; }
        IQuestionRepository QuestionRepository { get; set; }
        ITestThemeRepository TestThemeRepository { get; set; }
        TDto ToDto(TEntity entity);
        Task<TEntity> ToEntityAsync(TDto dto);
    }
}