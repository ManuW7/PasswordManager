using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    // Класс для взаимодействия с пользователем для входа в приложение
    // Устанавливает мастер пароль, если его еще нет, или проверяет его правильность, если он уже есть


    internal class EnterSecurer
    {
        // Поле для хранения мастер пароля
        readonly private MasterPassword masterPassword = new MasterPassword();

        // Создадим внутренний класс для хранения мастер пароля
        class MasterPassword
        {
            // В полях у него файл, где хранится пароль
            readonly private FileInfo masterPasswordFile = new FileInfo("mp.txt");

            // Метод, принимающий пароль, который нужно записать в файл
            public void CreatePassword (string password)
            {
                // Создаем сам файл, на который ссылается созданый экземпляр класса FileInfo
                FileStream fs = this.masterPasswordFile.Create();
                fs.Close();
                // Шифруем пароль и записываем его в файл
                string hashedPassword = Encrypter.HashPassword(password);
                using (StreamWriter sw = this.masterPasswordFile.CreateText())
                {
                    sw.WriteLine(hashedPassword);
                }
                this.masterPasswordFile.Refresh();
            }

            // Метод, возвращающий bool существует пароль или нет
            public bool Exists()
            {
                if (this.masterPasswordFile.Exists)
                {
                    return true;
                }
                return false;
            }

            // Метод для проверки, совпадает ли записаный в файл мастер пароль с передаваемым в метод
            public bool EqualsToGivenPassword(string passwordToCompare)
            {
                // Нужно достать пароль из файла
                using (StreamReader sr = this.masterPasswordFile.OpenText())
                {
                    // Достаем хэшированный пароль
                    string setPassword = sr.ReadLine();
                    // Вызываем метод, сверяющий пароли
                    return Encrypter.VerifyHashedPassword(setPassword, passwordToCompare);

                }
            }

        }

        // Метод, который будем вызывать для попытки входа
        public void Enter()
        {
            //Нужно проверить, установлен ли уже мастер пароль
            if (this.masterPassword.Exists())
            {
                // Если уже установлен, то запросить его ввод
                // Коммуницировать с пользователем должен юзер интерфейс
                // Получаем в перменную пароль
                UserInterface.PrintMessage("Введите мастер пароль для доступа к системе: ");
                string givenMasterPassword = UserInterface.AskForInformation();
                // Нужно проверить его на совпадение с записаным мастер паролем
                while (!(this.masterPassword.EqualsToGivenPassword(givenMasterPassword)))
                {
                    // Пока введенный пароль не будет совпадать, будем сообщать об этом пользователю и просить новый пароль
                    UserInterface.PrintMessage("Введенный мастер пароль не совпадает с установленным");
                    UserInterface.PrintMessage("Введите мастер пароль для доступа к системе: ");
                    givenMasterPassword = UserInterface.AskForInformation();
                }
                // Если прошли этот цикл, дать доступ в систему
                UserInterface.PrintMessage("Вы успешно вошли в систему, добро пожаловать");


            }
            else
            {
                // Если еще не установлен, то попросить придумать пароль
                // Коммуницировать с пользователем должен юзер интерфейсz
                UserInterface.PrintMessage("Установите мастер пароль: ");
                UserInterface.PrintMessage("Требования для пароля: длина не менее 16 символов, присутствие букв верхнего и нижнего регистров, а также цифр и специальных знаков");
                UserInterface.PrintMessage("Специальные знаки: !@#$%&*?");
                string proposedPassword = UserInterface.AskForInformation();
                // Введенный пароль должен проверяться на корректность
                while (!(PasswordUsabilityChecker.CheckTheMasterPassword(proposedPassword)))
                {
                    // Пока пароль не будет корректным, сообщать об этом пользователю и запрашивать его снова
                    UserInterface.PrintMessage("Пароль не подходит по требованиям безопасности, попробуйте снова");
                    UserInterface.PrintMessage("Установите мастер пароль: ");
                    UserInterface.PrintMessage("Требования для пароля: длина не менее 16 символов, присутствие букв верхнего и нижнего регистров, а также цифр и специальных знаков");
                    UserInterface.PrintMessage("Специальные знаки: !@#$%&*?");
                    proposedPassword = UserInterface.AskForInformation();
                }
                // Если прошли цикл, устанавливаем мастер пароль
                masterPassword.CreatePassword(proposedPassword);
                UserInterface.PrintMessage("Мастер пароль установлен");
                this.Enter();
            }

        }

    }
}
