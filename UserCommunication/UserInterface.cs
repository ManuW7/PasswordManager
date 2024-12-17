using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    internal class UserInterface
    {
        public static void PrintMessage(string str)
        {
            Console.WriteLine(str);
        }

        public static string AskForInformation()
        {
            return Console.ReadLine();
        }
    }
}
