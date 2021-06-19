using Microsoft.EntityFrameworkCore;
using Tests_CRUD_DAL.Entities;

namespace Tests_CRUD_DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestTheme> TestThemes { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
