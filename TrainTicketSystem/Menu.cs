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
        private int[] supportedMenuTypes = { MENU_TYPE_NAGIVATION,
                                               MENU_TYPE_DISPLAY };

        // Properties
        public string MenuName { get; set; }
        public string[] Options { get; set; }
        public int MenuType { get; set; }

        //Constructor
        public Menu(string menuName, string[] options, int menuType)
        {
            // Not a menu type we support?
            if (!supportedMenuTypes.Contains(menuType))
            {
                throw new Exception($"'{menuName}' Menu Type passed into Menu object was not valid. See constants in menu.cs for types.");
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
            for (int i = 0; i < Options.Length; i++)
            {
                if (menuType == Menu.MENU_TYPE_NAGIVATION) Console.WriteLine("{0} - {1}", i + 1, Options[i]);
                else Console.WriteLine("{0}", Options[i]);
            }
            if (menuType == Menu.MENU_TYPE_NAGIVATION) Console.WriteLine("\n0 - Exit program");
            else Console.WriteLine("\n0 - Return to the main menu");
            Console.WriteLine("-------------------");
            Console.Write("Please select an option: ");
        }
    }
}
