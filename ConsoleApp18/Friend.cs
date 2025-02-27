using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class Friend : PhoneBookEntry
    {
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Friend(string lastName, string address, string phoneNumber, DateTime birthday)
            : base(address, phoneNumber)
        {
            LastName = lastName;
            Birthday = birthday;
        }

        public override String ToString()
        {
            return ("Тип: Друг" + $"\nФамилия: {LastName}" + $"\nАдрес: {Address}" + $"\nТелефон: {PhoneNumber}" + $"\nДата рождения: {Birthday.ToShortDateString()}"+'\n' + new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            return LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
