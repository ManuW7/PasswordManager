using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    // Класс для проверки корректности и адекватности введенного пароля
    internal class PasswordUsabilityChecker
    {
        public static bool CheckTheMasterPassword(string password)
        {
            // Переменная, проверяющая, подходит ли пароль по длине
            bool isntTooShort = (password.Length >= 16);
            // Переменная, проверяющая наличие чисел:
            bool numbersArePresent = false;
            for (int i = 0; i < 10; i++)
            {
                if (password.Contains(i.ToString())){
                    numbersArePresent = true;
                }
            }
            // Переменная, проверяющая наличие специальных знаков
            bool specialSymbolsArePresent = false;
            char[] specialSymbols = new char[] { '!', '@', '#', '$', '%', '&', '*', '?' };
            foreach (var item in specialSymbols) 
            {
                if (password.Contains(item))
                {
                    specialSymbolsArePresent = true;
                }
            }
            //Переменная, проверяющая на наличие букв верхних и нижних регистров
            bool upperAndLowerCaseLettersArePresent = false;
            foreach (var item in password)
            {
                if (Char.IsLower(item) | Char.IsUpper(item))
                {
                    upperAndLowerCaseLettersArePresent = true;
                }
            }

            // Если все условия выполнены
            if (isntTooShort & specialSymbolsArePresent & upperAndLowerCaseLettersArePresent & numbersArePresent)
            {
                return true;
            }
            return false;
        }

        public static bool CheckInformationForSpaces(string stuff)
        {
            if (stuff.Contains(" "))
            {
                return false;
            }
            return true;
        }
    }
}
