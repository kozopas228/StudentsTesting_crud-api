using System;
using System.Collections.Generic;
using System.Text;
using Tests_CRUD_DAL.Entities.Util;

namespace Tests_CRUD_DAL.Entities
{
    public class TestTheme : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Test> Tests { get; set; }
    }
}
