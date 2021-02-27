using System;
using System.Collections.Generic;

namespace Tests_CRUD_BLL.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public Guid TestThemeId { get; set; }
    }
}
