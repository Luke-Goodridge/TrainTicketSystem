using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Train
    {
        private const int MAX_TRAIN_SIZE = 100;
        private const int ROWSIZE = 4;
        private const int SEATS_PER_ISLE = ROWSIZE / 2;

        // Setup various properties for the train
        public Seat[] SeatList { get; set; }
        public int TrainSize { get; }
        public int CarriageSize { get; }
        public int RowSize { get; }
        public int SeatsPerAisle { get; }

        // Constructor for the train seats.
        public Train(int trainSize, int carriageSize)
        {
            // Check train size is appropriate
            if (trainSize % ROWSIZE != 0) throw new Exception($"Train size ({trainSize}) must divide equally with rowSize ({ROWSIZE}).");
            if (carriageSize % ROWSIZE != 0) throw new Exception($"Carriage size ({carriageSize}) must divide equally with rowSize ({ROWSIZE}).");
            if (trainSize > MAX_TRAIN_SIZE) throw new Exception($"Trainsize ({trainSize}) is too big. Limit is {MAX_TRAIN_SIZE}");
            // TODO: Add in the ability to only have rows of divisible by 4

            // Set properties
            TrainSize = trainSize;
            CarriageSize = carriageSize;
            RowSize = ROWSIZE;
            SeatsPerAisle = SEATS_PER_ISLE;

            // Setup seats
            SetupSeats();
        }

        //Setup seats in the train.
        private void SetupSeats()
        {
            int firstClassSeatsAssigned = 0;

            SeatList = new Seat[TrainSize];
            for (int seatNumber = 0; seatNumber < TrainSize; seatNumber++)
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

        // Print out a nice console view of the seats.
        public void PrintSeatList()
        {
            //Key for the train seat ascii
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[XX] = First class seat");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[XX] = Seat already booked\n");
            Console.ForegroundColor = ConsoleColor.White;

            //Show the total wallet amount
            Console.WriteLine("Total in wallet: {0}\n", Wallet.WalletTotal.ToString("c2")); ; ;

            //Loop through all the seats in the train
            int seatCount = 0;
            Console.WriteLine("  /^^^^^^^^^^^^\\");
            Console.WriteLine(" /--------------\\");
            Console.WriteLine("/----------------\\");
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

                // Do some nice formatting to the train view
                FormatTrainSeatView(seatCount);

            }
        }

        // Show a nice view of the seats to the user.
        public void ShowSeatBooking()
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
                if (Int32.TryParse(Console.ReadLine(), out int seatNumber) == false || seatNumber < returnToMenu || seatNumber > TrainSize)
                {
                    Console.WriteLine("\nThat is not a valid seat number. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                // Exit to main menu if they want
                else if (seatNumber == -1)
                {
                    Program.MainMenu();
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

        //Book seats on the train
        public void BookSeat(int seatNumber)
        {
            //Show the list again
            Console.Clear();
            PrintSeatList();

            // Setup a ticket price for the selected seat.
            Seat selectedSeat = SeatList[seatNumber];
            float priceOfSeat = Tickets.GetPrice(selectedSeat);

            // Lets show them the price first
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine($"The price for seat number {seatNumber} is [£{priceOfSeat}]");
            if (SeatList[seatNumber].IsFirstClass) Console.WriteLine("(This seat is first class)");
            Console.WriteLine("---------------------------------------------------");
            Console.Write($"Do you wish to continue with seat {seatNumber}? (y/n): ");
            if (Console.ReadLine().ToLower().Equals("y"))
            {
                // If they dont say no, confirm their booking
                // Check they have enough to actually buy the seat
                if (Wallet.DeductPrice(priceOfSeat) == true)
                {
                    SeatList[seatNumber].IsTaken = true;
                    Console.Clear();
                    PrintSeatList();
                    Console.WriteLine($"\n**** Booking confirmed for seat {seatNumber} for £{priceOfSeat} ****");

                    //return them to the main menu
                    Console.Write("\n\nEnter -1 to go back to the main menu.\nOtherwise press enter to rebook another seat: ");
                    if (Console.ReadLine() == "-1")
                        Program.MainMenu();
                    else ShowSeatBooking();
                }
                else
                {
                    Console.WriteLine("\nI'm Afraid you dont have enough in your wallet to book this seat.");
                    Console.WriteLine("Press any key to pick another seat.");
                    Console.ReadKey();
                    ShowSeatBooking();
                }
            }
            // If they dont want it, lets go back to the seat view.
            else
            {
                ShowSeatBooking();
            }
        }

        private void FormatTrainSeatView(int seatCount)
        {
            // Add a little label showing the front
            if (seatCount == RowSize)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  <----FRONT");
            }
            // Add the aisle
            else if (seatCount % SeatsPerAisle == 0 && seatCount % RowSize != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("| |");
            }

            // We seperate out the seats into carriages
            if (seatCount % CarriageSize == 0 && seatCount != 0 && seatCount != TrainSize)
            {
                string carriageSeperator = "";
                Console.ForegroundColor = ConsoleColor.White;
                // Should scale with the rowsize
                for (int i = 0; i < RowSize * RowSize - 1; i++)
                {
                    carriageSeperator = $"{carriageSeperator}-";
                }
                Console.Write($"\n[|{carriageSeperator}|]");
            }
            // Add a little label showing the back
            else if (seatCount == TrainSize)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  <----BACK");
                Console.WriteLine("\\                 /");
                Console.WriteLine(" \\_______________/");

            }
        }
    }
}
