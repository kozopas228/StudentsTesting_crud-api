using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_CRUD_DAL.Entities
{
    public class TestTheme
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Test> Tests { get; set; }
    }
}
