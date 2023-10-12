using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMSalesman
{
    public class Salesman
    {
        public Salesman(Tuple<int, int> position)
        {
            Position = position;
        }

        public Tuple<int, int> Position { get; set; }
    }
}
