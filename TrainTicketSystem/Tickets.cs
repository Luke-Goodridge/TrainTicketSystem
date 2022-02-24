using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Tickets
    {
        // Prices of the tickets, these shouldnt change.
        // Though functionality for an admin to change them might come in future...
        private const float FIRST_CLASS_PRICE = 9.99f;
        private const float STANDARD_CLASS_PRICE = 3.99f;
        private const float PEAK_TICKET_PRICE = 7.99f;
        private const float OFF_PEAK_TICKET_PRICE = 2.99f;

        // Get the price of an individual seat
        public static float GetPrice(Seat seat)
        {
            if (seat.IsFirstClass) return FIRST_CLASS_PRICE;
            else return STANDARD_CLASS_PRICE;
        }

        // Get all prices in an array format.
        private static float[] GetAllTicketPrices
        {
            get
            {
                return new float[] { FIRST_CLASS_PRICE,
                                     STANDARD_CLASS_PRICE,
                                     PEAK_TICKET_PRICE,
                                     OFF_PEAK_TICKET_PRICE };
            }
        }

        // We might want to format the prices to be viewed nicely on a menu
        public static string[] GetTicketOptions()
        {
            // Get our prices, and then make a new array to hold the strings
            float[] ticketPrices = GetAllTicketPrices;
            string[] options = new string[ticketPrices.Length];
            string ticketDescription = "";

            // Loop through and make a nice array of strings we can use in the menu display
            for (int i = 0; i < ticketPrices.Length; i++)
            {
                options[i] = "£" + ticketPrices[i].ToString();
                switch (ticketPrices[i])
                {
                    case FIRST_CLASS_PRICE:
                        ticketDescription = "First Class";
                        break;
                    case STANDARD_CLASS_PRICE:
                        ticketDescription = "Standard";
                        break;
                    case PEAK_TICKET_PRICE:
                        ticketDescription = "Peak";
                        break;
                    case OFF_PEAK_TICKET_PRICE:
                        ticketDescription = "Off-Peak";
                        break;
                    default:
                        throw new Exception("Ticket price didnt have a valid price.");
                }
                // Combine the price and the description
                options[i] += $" - {ticketDescription}";
            }
            return options;
        }

    }
}
