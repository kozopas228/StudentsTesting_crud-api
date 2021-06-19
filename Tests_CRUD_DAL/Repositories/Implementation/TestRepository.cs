using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests_CRUD_DAL.Entities;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_DAL.Repositories.Implementation
{
    public class TestRepository : ITestRepository
    {
        private ApplicationContext _context;

        public TestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.Include(x => x.Questions).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Test obj)
        {
            var test = await _context.Tests.FindAsync(obj.Id);
            if (test == null)
            {
                return false;
            }

            test.TestThemeId = obj.TestThemeId;
            test.Description = obj.Description;
            test.Name = obj.Name;

            test.TestTheme = await _context.TestThemes.FindAsync(obj.TestThemeId);
            test.Questions = obj.Questions;

            _context.Tests.Update(test);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> CreateAsync(Test obj)
        {
            await _context.Tests.AddAsync(obj);
            await _context.SaveChangesAsync();

            return obj.Id;
        }
    }
}