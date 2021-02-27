using System;

namespace Tests_CRUD_BLL.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
    }
}