using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tests_CRUD_DAL.Entities;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_DAL.Repositories.Implementation
{
    public class TestRepository : ITestRepository
    {
        private ApplicationContext _context;

        public TestRepository()
        {
            _context ??= new ApplicationContext();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
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
            test.Questions = _context.Questions.Where(x => x.TestId == obj.Id);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task CreateAsync(Test obj)
        {
            await _context.Tests.AddAsync(obj);
            await _context.SaveChangesAsync();
        }
    }
}