using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterface.PrintMessage("Добро пожаловать в менеджер паролей");
            EnterSecurer es = new EnterSecurer();
            es.Enter();
            Processor psc = new Processor();
            psc.Work();


            //TODO: async

            //TODO: interfaces

            Console.ReadLine();
        }
    }
}
