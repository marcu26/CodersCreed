using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Pageable
{
    public class PageableResponse
    {
        public int draw { get; set; }

        public int recordTotal { get; set; }

        public int recordsFiltered { get; set; }

        public object data { get; set; }
    }
}
