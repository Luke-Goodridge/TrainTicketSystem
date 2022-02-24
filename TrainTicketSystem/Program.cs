using System;
using System.Diagnostics;

namespace TrainTicketSystem
{
    internal class Program
    {
        private static Train train = new Train(75, 25, 5);

        static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            int optionNumber = -1;

            // Create the main menu
            Menu mainMenu = new("Main Menu", new string[] { "1 - SeatMenu", "2 - TicketMenu" }, Menu.MENU_TYPE_NAGIVATION);

            // Show the main menu first
            mainMenu.BuildMenu(mainMenu.MenuType);

            // Users can enter 0 to exit the program
            while (optionNumber != 0)
            {
                if (Int32.TryParse(Console.ReadLine(), out optionNumber) == false || optionNumber > mainMenu.Options.Length || optionNumber < 0)
                {
                    mainMenu.BuildMenu(mainMenu.MenuType);
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
                        train.ShowSeatBooking();
                        break;
                    case 2:
                        TicketPriceMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void TicketPriceMenu()
        {
            int option = -1;
            Menu ticketMenu = new("Ticket Price Menu", Tickets.GetTicketOptions(), Menu.MENU_TYPE_DISPLAY);
            ticketMenu.BuildMenu(ticketMenu.MenuType);

            while (option != 0)
            {
                if (Console.ReadLine().Equals("0"))
                {
                    MainMenu();
                }
                else
                {
                    ticketMenu.BuildMenu(ticketMenu.MenuType);
                    Console.Write("\nThat is not a valid option, use 0 to go back to the main menu: ");
                    continue;
                }
            }
        }
    }
}
