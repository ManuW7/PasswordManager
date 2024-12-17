using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    internal class PasswordBase
    {
        private FileInfo passwordsFile = new FileInfo("passwords.txt");
        
        public PasswordBase()
        {
            if (!(this.passwordsFile.Exists))
            {
                FileStream fs = this.passwordsFile.Create();
                fs.Close();
            }
        }

        public int GetPasswordsAmount()
        {
            int amount = 0;
            using (StreamReader sr = this.passwordsFile.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    amount++;
                }
            }
            return amount;
        }

        public void AddPassword(string login, string password) 
        {
            using (StreamWriter sw = this.passwordsFile.AppendText())
            {
                sw.WriteLine(login + " " + password);
            }
            this.passwordsFile.Refresh();
        }

        public string[] SearchForPasswords (string login)
        {
            string[] matchedPasswords = new string[0];
            using (StreamReader sr = passwordsFile.OpenText())
            {
                string line;
                string[] currentPair;
                while ((line = sr.ReadLine()) != null)
                {
                    currentPair = line.Split();
                    if (currentPair[0] == login)
                    {
                        string[] newMatchedPasswords = new string[matchedPasswords.Length + 1];
                        for (int i = 0; i < matchedPasswords.Length; i++)
                        {
                            newMatchedPasswords[i] = matchedPasswords[i];
                        }
                        newMatchedPasswords[matchedPasswords.Length] = Coder.Decrypt(currentPair[1]);
                        matchedPasswords = newMatchedPasswords;
                    }
                }
            }
            return matchedPasswords;
        }

        public void ChangePasswords(string login)
        {
            string[] passwords = new string[0];
            using (StreamReader sr = passwordsFile.OpenText())
            {
                string line;
                string[] currentPair;
                while ((line = sr.ReadLine()) != null)
                {
                    currentPair = line.Split();
                    if (currentPair[0] == login)
                    {
                        UserInterface.PrintMessage("Найдена запись по логину: " + login + " текущий пароль: " + Coder.Decrypt(currentPair[1]));
                        UserInterface.PrintMessage("Введите новый пароль");
                        string newPassword = UserInterface.AskForInformation();
                        string[] newPasswords = new string[passwords.Length + 1];
                        for (int i = 0; i < passwords.Length; i++)
                        {
                            newPasswords[i] = passwords[i];
                        }
                        newPasswords[newPasswords.Length - 1] = login + " " + Coder.Encrypt(newPassword);
                        passwords = newPasswords;
                    }
                    else
                    {
                        string[] newPasswords = new string[passwords.Length + 1];
                        for (int i = 0; i < passwords.Length; i++)
                        {
                            newPasswords[i] = passwords[i];
                        }
                        newPasswords[newPasswords.Length - 1] = currentPair[0] + " " + currentPair[1];
                        passwords = newPasswords;
                    }
                }
            }
            // В passwords теперь записан массив строк, которые должны быть записаны в файл
            // Удалим исходный файл, создадим новый с обновленными паролями
            this.passwordsFile.Delete();
            FileStream fs = this.passwordsFile.Create();
            fs.Close();
            using (StreamWriter sw = this.passwordsFile.AppendText())
            {
                foreach (string s in passwords)
                {
                    sw.WriteLine(s);
                }
            }
            this.passwordsFile.Refresh();
        }
    }
}
