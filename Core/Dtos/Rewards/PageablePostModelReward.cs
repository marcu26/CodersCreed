using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Rewards
{
    public class PageablePostModelReward
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public int? userId { get; set; }
    }
}
