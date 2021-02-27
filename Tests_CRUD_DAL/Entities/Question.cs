using System;
using System.Collections.Generic;
using System.Text;
using Tests_CRUD_DAL.Entities.Util;

namespace Tests_CRUD_DAL.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public Guid? TestId { get; set; }
        public Test Test { get; set; }
    }
}
