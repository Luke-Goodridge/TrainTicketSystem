using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Menu
    {
        // Types of menu we might have
        public const int MENU_TYPE_NAGIVATION = 0;
        public const int MENU_TYPE_DISPLAY = 1;

        // Properties
        public string MenuName { get; set; }
        public string[] Options { get; set; }
        public int MenuType { get; set; }

        //Constructor
        public Menu(string menuName, string[] options, int menuType)
        {
            if (menuType == MENU_TYPE_NAGIVATION)
            {
                // Validate the options are appropriate
                foreach (string option in options)
                {
                    // Check if the first character of the menu option is a number, othewise throw an exception
                    if (Int32.TryParse(option[0].ToString(), out int menuOptionNumber) == false)
                    {
                        throw new Exception($"'{option}' must start with a number to indicate what the user should input.");
                    }
                }
            }
            // Assign our menu properties
            MenuName = menuName;
            Options = options;
            MenuType = menuType;
        }

        public void BuildMenu(int menuType)
        {
            Console.Clear();
            Console.WriteLine($"{MenuName}");
            Console.WriteLine("-------------------\n");
            foreach (string option in Options)
            {
                Console.WriteLine(option);
            }
            if (menuType == Menu.MENU_TYPE_NAGIVATION) Console.WriteLine("\n0 - Exit program");
            else Console.WriteLine("\n0 - Return to the main menu");
            Console.WriteLine("-------------------");
            Console.Write("Please select an option: ");
        }
    }
}
