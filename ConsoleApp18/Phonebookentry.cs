using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public abstract class PhoneBookEntry
    {
        // Адрес и телефон есть у любого наследника
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // Конструктор базового класса
        protected PhoneBookEntry(string address, string phoneNumber)
        {
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Метод для вывода информации на экран (должен быть реализован в потомках)
        public abstract override String ToString();

        // Метод для проверки соответствия поисковому критерию (например, фамилии)
        public abstract bool MatchesCriteria(string lastName);
    }
}
