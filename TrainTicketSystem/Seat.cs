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
        public bool IsFirstClass { get; set; }
        public int ID { get; set; }

        // Constructors
        public Seat(int id)
        {
            IsTaken = false;
            ID = id;
        }

        //If we happen to have defined first class too.
        public Seat(int id, bool isFirstClass)
        {
            IsFirstClass = isFirstClass;
            IsTaken = false;
            ID = id;
        }
    }
}
