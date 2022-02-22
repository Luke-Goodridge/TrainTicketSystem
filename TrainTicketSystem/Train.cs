using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Train
    {
        // Setup various properties for the train
        public Seat[] SeatList { get; set; }
        public int TrainSize { get; }
        public int CarriageSize { get; }
        public int RowSize { get; }

        private const int MAX_TRAIN_SIZE = 100;

        // Constructor for the train seats.
        public Train(int trainSize, int carriageSize, int rowSize)
        {
            // Check train size is appropriate
            if (trainSize % rowSize != 0) throw new Exception($"Train size ({trainSize}) must divide equally with rowSize ({rowSize}).");
            if (carriageSize % rowSize != 0) throw new Exception($"Carriage size ({carriageSize}) must divide equally with rowSize ({rowSize}).");
            if (trainSize > MAX_TRAIN_SIZE) throw new Exception($"Trainsize ({trainSize}) is too big. Limit is {MAX_TRAIN_SIZE}");

            // Set properties
            TrainSize = trainSize;
            CarriageSize = carriageSize;
            RowSize = rowSize;

            // Setup seats
            SetupSeats();
        }

        // Print out a nice console view of the seats.
        public void PrintSeatList()
        {
            int seatCount = 0;
            foreach (Seat seat in SeatList)
            {
                //If the seat number is 1 digit, lets add a 0 for formatting
                string isTakenIndicator = seat.ID < 10 ? "0" + seat.ID.ToString() : seat.ID.ToString();

                // Check if the seat is taken - TODO - change this to colours
                isTakenIndicator = seat.IsTaken ? "X" : isTakenIndicator;

                // Recolour the first class seats
                Console.ForegroundColor = seat.IsFirstClass ? ConsoleColor.Yellow : ConsoleColor.White;

                // If we reach the end of a row, start a new line
                if (seatCount % RowSize == 0)
                {
                    Console.Write($"\n[{isTakenIndicator}]");
                }
                else
                {
                    Console.Write($"[{isTakenIndicator}]");
                }
                seatCount++;

                // We seperate out the seats into carriages
                if (seatCount % CarriageSize == 0)
                {
                    string carriageSeperator = "";
                    Console.ForegroundColor = ConsoleColor.White;
                    // Should scale with the rowsize
                    for (int i = 0; i < RowSize * 4; i++)
                    {
                        carriageSeperator = $"{carriageSeperator}-";
                    }
                    Console.Write($"\n{carriageSeperator}");
                }
            }
        }

        //Setup seats in the train.
        private void SetupSeats()
        {
            int firstClassSeatsAssigned = 0;
            bool firstClassRow = false;

            SeatList = new Seat[TrainSize];
            for (int seatNumber = 0; seatNumber < SeatList.Length; seatNumber++)
            {
                // Make the first row of each carriage a first class row
                if (seatNumber % CarriageSize == 0 || firstClassRow)
                {
                    SeatList[seatNumber] = new Seat(seatNumber, true);

                    //Now lets make sure to get the following seats on that row
                    firstClassRow = true;
                    firstClassSeatsAssigned++;
                    if (firstClassSeatsAssigned == RowSize)
                    {
                        firstClassRow = false;
                        firstClassSeatsAssigned = 0;
                    }
                }
                else
                {
                    SeatList[seatNumber] = new Seat(seatNumber);
                }
            }
        }
    }
}
