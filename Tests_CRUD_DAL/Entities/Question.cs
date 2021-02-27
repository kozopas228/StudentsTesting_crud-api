using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_CRUD_DAL.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
    }
}
