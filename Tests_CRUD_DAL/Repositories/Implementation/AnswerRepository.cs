using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tests_CRUD_DAL.Entities;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_DAL.Repositories.Implementation
{
    public class AnswerRepository : IAnswerRepository
    {
        private ApplicationContext _context;

        public AnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return false;
            }

            _context.Answers.Remove(answer);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Answer obj)
        {
            var answer = await _context.Answers.FindAsync(obj.Id);
            if (answer == null)
            {
                return false;
            }

            answer.QuestionId = obj.QuestionId;
            answer.IsCorrect = obj.IsCorrect;
            answer.Text = obj.Text;

            answer.Question = await _context.Questions.FindAsync(obj.QuestionId);

            _context.Answers.Update(answer);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> CreateAsync(Answer obj)
        {
            await _context.Answers.AddAsync(obj);

            await _context.SaveChangesAsync();

            return obj.Id;
        }
    }
}