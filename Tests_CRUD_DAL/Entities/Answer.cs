using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tests_CRUD_DAL.Entities.Util;

namespace Tests_CRUD_DAL.Entities
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
