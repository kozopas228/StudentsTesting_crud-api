using System;
using System.Collections.Generic;
using Tests_CRUD_DAL.Entities.Util;

namespace Tests_CRUD_DAL.Entities
{
    public class Test : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }
        public Guid? TestThemeId { get; set; }
        public TestTheme TestTheme { get; set; }
    }
}
