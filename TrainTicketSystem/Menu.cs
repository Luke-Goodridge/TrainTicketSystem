using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Menu
    {
        // Properties
        public string MenuName { get; set; }
        public string[] Options { get; set; }

        //Constructor
        public Menu(string menuName, string[] options)
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

            // Assign our menu properties
            MenuName = menuName;
            Options = options;
            Console.WriteLine(options);
            Console.WriteLine(Options);

        }

        public void BuildMenu()
        {
            Console.Clear();
            Console.WriteLine($"{MenuName}");
            Console.WriteLine("-------------------");
            foreach (string option in Options)
            {
                Console.WriteLine(option);
            }
            Console.WriteLine("0 - Exit program");
            Console.WriteLine("-------------------");
            Console.Write("Please select an option: ");
        }
    }
}
