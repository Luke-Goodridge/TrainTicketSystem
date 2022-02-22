using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Ticket
    {
        private const float FIRST_CLASS_PRICE = 9.99f;
        private const float STANDARD_CLASS_PRICE = 3.99f;

        // Get the price of an individual seat
        public float GetPrice(Seat seat)
        {
            if (seat.IsFirstClass) return FIRST_CLASS_PRICE;
            else return STANDARD_CLASS_PRICE;
        }

        // Get all prices.
        public float[] GetAllTicketPrices
        {
            get
            {
                return new float[] { FIRST_CLASS_PRICE, STANDARD_CLASS_PRICE };
            }
        }
    }
}
