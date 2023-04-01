using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Pageable
{
    public class PageableDto<T>
    {
        public int NumarTotalRanduri { get; set; }

        public int NumarRanduriFiltrate { get; set; }

        public List<T> Pagina { get; set; }
    }
    
}
