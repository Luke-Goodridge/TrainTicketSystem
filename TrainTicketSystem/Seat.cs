using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Seat
    {
        public bool IsTaken { get; set; }
        public string Class { get; set; }

        // Constructor 
        public Seat()
        {
            IsTaken = false;
        }
    }
}
