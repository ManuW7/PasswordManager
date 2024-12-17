using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    // Класс, который будет собственно выполнять всю логическую работу приложения
    internal class Processor
    {
        private PasswordBase pb = new PasswordBase();
        public void Work()
        {
            // При запуске процессора кидаем пользователя в главное меню
            MainMenu.ShowMenu();
            // Заправшиваем у пользователя ввод команды, пока ввод не будет корректным
            string answer = MainMenu.GetAnswer();
            // Запускаем цикл while, который и будет по сути работой программы
            // Работать она будет до тех пор, пока не поступит команда завершить работу
            while (answer != "4")
            {
                this.DoTheCommand(answer);
                MainMenu.ShowMenu();
                answer = MainMenu.GetAnswer();
            }
            UserInterface.PrintMessage("Спасибо за использование нашей программы");
        }

        public void DoTheCommand(string command)
        {
            switch (command)
            {
                case "1":
                    this.AddNewPassword();
                    break;
                case "2":
                    this.SearchForPassword();
                    break;
                case "3":
                    this.ChangeThePasword();
                    break;
            }
        }

        public void AddNewPassword()
        {
            // Чтобы добавить новый пароль, нужно спросить логин, для которого будет сохраняться пароль
            UserInterface.PrintMessage("Введите логин, пароль для которого хотите записать: ");
            string login = UserInterface.AskForInformation();
            while (!(PasswordUsabilityChecker.CheckInformationForSpaces(login)))
            {
                UserInterface.PrintMessage("Введите логин без пробелов");
                UserInterface.PrintMessage("Введите логин, пароль для которого хотите записать: ");
                login = UserInterface.AskForInformation();
            }
            // И непосредственно сам пароль
            UserInterface.PrintMessage("Введите пароль: ");
            string password = UserInterface.AskForInformation();
            while (!(PasswordUsabilityChecker.CheckInformationForSpaces(password)))
            {
                UserInterface.PrintMessage("Введите пароль без пробелов");
                UserInterface.PrintMessage("Введите пароль: ");
                password = UserInterface.AskForInformation();
            }
            // После чего передаем в базу с паролями логин и зашифрованный пароль
            this.pb.AddPassword(login, Coder.Encrypt(password));
            UserInterface.PrintMessage("Пароль успешно добавлен");
        }

        public void SearchForPassword()
        {
            if (this.pb.GetPasswordsAmount() == 0)
            {
                UserInterface.PrintMessage("В системе еще не записано никаких данных о паролях, воспользуйтесь командой 1");
            }
            else
            {
                UserInterface.PrintMessage("Введите логин для поиска пароля");
                string login = UserInterface.AskForInformation();
                string[] foundPasswords = this.pb.SearchForPasswords(login);
                if (foundPasswords.Length == 0)
                {
                    UserInterface.PrintMessage("Не было найденно данных по данному логину");
                }
                else
                {
                    UserInterface.PrintMessage("Были найдены следующие пароли: ");
                    foreach (string s in foundPasswords)
                    {
                        UserInterface.PrintMessage(s);
                    }
                }
            }
        }

        public void ChangeThePasword()
        {
            if (this.pb.GetPasswordsAmount() == 0)
            {
                UserInterface.PrintMessage("В системе еще не записано никаких данных о паролях, воспользуйтесь командой 1");
            }
            else
            {
                UserInterface.PrintMessage("Введите логин для изменения пароля");
                string login = UserInterface.AskForInformation();
                this.pb.ChangePasswords(login);
            }
        }
    }
}
