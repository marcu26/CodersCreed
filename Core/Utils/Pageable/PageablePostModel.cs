using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Pageable
{
    public class PageablePostModel
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        //public List<Column> columns { get; set; }

        //public Search search { get; set; }

        //public List<Dictionary<string, string>> order { get; set; }
    }
}
