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
            // TODO: Add in the ability to only have rows of divisible by 2

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
            //Key for the train seat ascii
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[XX] = First class seat");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[XX] = Seat already booked\n");
            Console.ForegroundColor = ConsoleColor.White;

            //Loop through all the seats in the train
            int seatCount = 0;
            foreach (Seat seat in SeatList)
            {
                //If the seat number is 1 digit, lets add a 0 for formatting
                string seatNumber = seat.ID < 10 ? "0" + seat.ID.ToString() : seat.ID.ToString();

                // Recolour the seat if its taken
                if (seat.IsTaken)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                // Recolour the seat if its first class
                else if (seat.IsFirstClass)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else Console.ForegroundColor = ConsoleColor.White;

                // If we reach the end of a row, start a new line
                if (seatCount % RowSize == 0 && seatCount != 0)
                {
                    Console.Write($"\n[{seatNumber}]");
                }
                else
                {
                    Console.Write($"[{seatNumber}]");
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

            SeatList = new Seat[TrainSize];
            for (int seatNumber = 0; seatNumber < SeatList.Length; seatNumber++)
            {
                // Make the first row of each carriage a first class row
                if (seatNumber % CarriageSize == 0 || firstClassSeatsAssigned > 0)
                {
                    SeatList[seatNumber] = new Seat(seatNumber, true);

                    //Now lets make sure to get the following seats on that row
                    firstClassSeatsAssigned++;
                    if (firstClassSeatsAssigned == RowSize)
                    {
                        firstClassSeatsAssigned = 0;
                    }
                }
                else
                {
                    SeatList[seatNumber] = new Seat(seatNumber);
                }
                // For testing sake, lets add a few random seats that are taken already
                Random random = new Random();
                int randomSeatRate = SeatList.Length / 7;
                if (random.Next(randomSeatRate) == 0)
                {
                    SeatList[seatNumber].IsTaken = true;
                }
            }
        }

        //Book seats on the train
        public void BookSeat(int seatNumber)
        {
            //Show the list again
            Console.Clear();
            PrintSeatList();

            // Setup a ticket price for the selected seat.
            Seat selectedSeat = SeatList[seatNumber];
            float priceOfSeat = Ticket.GetPrice(selectedSeat);

            // Lets show them the price first
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine($"The price for seat number {seatNumber} is [£{priceOfSeat}]");
            if (SeatList[seatNumber].IsFirstClass) Console.WriteLine("(This seat is first class)");
            Console.WriteLine("---------------------------------------------------");
            // If they dont want it, lets go back to the seat view.
            Console.Write($"Do you wish to continue with seat {seatNumber}? (y/n): ");
            if (Console.ReadLine().ToLower().Equals("n"))
            {
                ShowSeats();
            }
            // If they do want the seat, confirm their booking
            SeatList[seatNumber].IsTaken = true;
            Console.Clear();
            PrintSeatList();
            Console.WriteLine($"\n**** Booking confirmed for seat {seatNumber} for £{priceOfSeat} ****");

            //return them to the main menu
            Console.Write("\n\nPress any key to return to the main menu.");
            Console.ReadKey();
            Program.ProgramMainMenu();
        }

        // Show a nice view of the seats to the user.
        public void ShowSeats()
        {
            while (true)
            {
                // What key the user has to press to get back to the main menu
                int returnToMenu = -1;

                // Show the seats of the train.
                Console.Clear();
                PrintSeatList();

                // Ask what seat the user wants
                Console.WriteLine("\n\nChoose '-1' in order to return to the main menu.");
                Console.Write("Which seat would you like to book: ");

                // Check if the number is a valid seat.
                if (Int32.TryParse(Console.ReadLine(), out int seatNumber) == false || seatNumber < returnToMenu || seatNumber >= TrainSize)
                {
                    Console.WriteLine("\nThat is not a valid seat number. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                // Exit to main menu if they want
                else if (seatNumber == -1)
                {
                    Program.ProgramMainMenu();
                }
                // If the seat is taken, we cant book that one.
                else if (SeatList[seatNumber].IsTaken)
                {
                    Console.WriteLine("\nIm sorry but that seat is taken :( Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    // Its a valid seat so lets try to book it.
                    BookSeat(seatNumber);
                    break;
                }
            }
        }
    }
}
