using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_CRUD_DAL.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public Guid TestThemeId { get; set; }
        public TestTheme TestTheme { get; set; }
    }
}
