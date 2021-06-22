﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_DAL.Entities;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_DAL.Repositories.Implementation
{
    public class TestThemeRepository : ITestThemeRepository
    {
        private ApplicationContext _context;

        public TestThemeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestTheme>> GetAllAsync()
        {
            return await _context.TestThemes.Include(x => x.Tests).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var theme = await _context.TestThemes.FindAsync(id);
            if (theme == null)
            {
                return false;
            }

            _context.TestThemes.Remove(theme);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(TestTheme obj)
        {
            var theme = await _context.TestThemes.FindAsync(obj.Id);
            if (theme == null)
            {
                return false;
            }

            theme.Name = obj.Name;

            var tests = await _context.Tests.Where(x => x.TestThemeId == obj.Id).ToListAsync();

            theme.Tests = tests;

            _context.TestThemes.Update(theme);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> CreateAsync(TestTheme obj)
        {
            await _context.TestThemes.AddAsync(obj);
            await _context.SaveChangesAsync();

            return obj.Id;
        }
    }
}