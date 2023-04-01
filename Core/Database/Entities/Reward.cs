using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Reward : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

    }
}
