using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Train
    {
        public Seat[] SeatList { get; set; }
        public int TrainSize { get; set; }

        // Constructor for the train seats.
        public Train()
        {
            TrainSize = 30;
            // Make an array of seats
            SeatList = new Seat[TrainSize];
            // Assign a seat to each element of the array.
            for (int i = 0; i < SeatList.Length; i++) SeatList[i] = new Seat();
        }

        // Print out a nice console view of the seats.
        public void PrintSeatList()
        {
            int rowLimit = 3;
            int seatCount = 0;
            foreach (Seat seat in SeatList)
            {
                // Check if the seat is taken
                string isTakenIndicator = !seat.IsTaken ? "O" : "X";
                if (seatCount % rowLimit == 0)
                    Console.Write($"\n[{isTakenIndicator}]");
                else Console.Write($"[{isTakenIndicator}]");
                seatCount++;
            }
        }
    }
}
