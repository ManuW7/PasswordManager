using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    // Класс главное меню. Работает с польщователем когда он заходит в систему и после выполнения уже поситавленой задачи
    internal class MainMenu
    {
        public static void ShowMenu()
        {
            UserInterface.PrintMessage("Выберите действие:");
            UserInterface.PrintMessage("1.Добавить новый пароль");
            UserInterface.PrintMessage("2.Поиск пароля");
            UserInterface.PrintMessage("3.Изменить пароль");
            UserInterface.PrintMessage("4.Выход");

        }
        public static string GetAnswer()
        {
            UserInterface.PrintMessage("Введите пожалуйста число от 1 до 4");
            string answer = UserInterface.AskForInformation();
            while ((answer != "1") & (answer != "2") & (answer != "3") & (answer != "4"))
            {
                UserInterface.PrintMessage("Неверно введены данные");
                UserInterface.PrintMessage("Введите пожалуйста число от 1 до 4");
                answer = UserInterface.AskForInformation();
            }
            return answer;
                
        }
    }
}
