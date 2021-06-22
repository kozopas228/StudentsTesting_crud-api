using System.Collections.Generic;
using Tests_CRUD_DAL.Entities.Util;

namespace Tests_CRUD_DAL.Entities
{
    public class TestTheme : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
