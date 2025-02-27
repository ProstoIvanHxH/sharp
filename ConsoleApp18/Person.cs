using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class Person : PhoneBookEntry
    {
        public string LastName { get; set; }

        public Person(string lastName, string address, string phoneNumber)
            : base(address, phoneNumber)
        {
            LastName = lastName;
        }

        public override String ToString()
        {
            return("Тип: Персона"+$"\nФамилия: {LastName}"+$"\nАдрес: {Address}"+$"\nТелефон: {PhoneNumber}"+'\n'+new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            // Простая проверка на совпадение фамилии
            return LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
