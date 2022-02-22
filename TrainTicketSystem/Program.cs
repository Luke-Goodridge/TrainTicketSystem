using System;

namespace TrainTicketSystem
{
    internal class Program
    {
        private static Train train = new Train(30, 12, 3);

        static void Main(string[] args)
        {
            ProgramMainMenu();
        }

        public static void ProgramMainMenu()
        {
            int optionNumber = -1;

            // Create the main menu
            Menu mainMenu = new Menu("Main Menu", new string[] { "1 - SeatMenu", "2 - TicketMenu" });

            // Show the main menu first
            mainMenu.BuildMenu();

            // Users can enter 0 to exit the program
            while (optionNumber != 0)
            {
                if (Int32.TryParse(Console.ReadLine(), out optionNumber) == false || optionNumber > mainMenu.Options.Length || optionNumber < 0)
                {
                    mainMenu.BuildMenu();
                    Console.Write("\n\nThat is not a valid option. Please try again: ");
                    optionNumber = -1;
                    continue;
                }
                switch (optionNumber)
                {
                    // Exit the program
                    case 0:
                        Environment.Exit(1);
                        break;
                    case 1:
                        train.ShowSeats();
                        break;
                    case 2:
                        // TODO: Add a ticket price view here.
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
