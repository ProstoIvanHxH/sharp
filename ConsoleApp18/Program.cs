using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PhoneBookExample
{
    // 1) Абстрактный класс телефонного справочника
    [Serializable]
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
        public abstract void PrintInfo();

        // Метод для проверки соответствия поисковому критерию (например, фамилии)
        public abstract bool MatchesCriteria(string lastName);
    }

    // 2) Класс "Персона"
    [Serializable]
    public class Person : PhoneBookEntry
    {
        public string LastName { get; set; }

        public Person(string lastName, string address, string phoneNumber)
            : base(address, phoneNumber)
        {
            LastName = lastName;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: Персона");
            Console.WriteLine($"Фамилия: {LastName}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine(new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            // Простая проверка на совпадение фамилии
            return LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Класс "Организация"
    [Serializable]
    public class Organization : PhoneBookEntry
    {
        public string OrganizationName { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }

        public Organization(
            string organizationName,
            string address,
            string phoneNumber,
            string fax,
            string contactPerson)
            : base(address, phoneNumber)
        {
            OrganizationName = organizationName;
            Fax = fax;
            ContactPerson = contactPerson;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: Организация");
            Console.WriteLine($"Название: {OrganizationName}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Факс: {Fax}");
            Console.WriteLine($"Контактное лицо: {ContactPerson}");
            Console.WriteLine(new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            // Организация обычно не ищется по фамилии, 
            // поэтому возвращаем false
            return false;
        }
    }

    // Класс "Друг" (фактически тот же Person, но с датой рождения)
    [Serializable]
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

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: Друг");
            Console.WriteLine($"Фамилия: {LastName}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Дата рождения: {Birthday.ToShortDateString()}");
            Console.WriteLine(new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            return LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            // 3) Формируем базу (список) из данных, считанных из файла
            
            
            string filePath = "input.dat"; // Задайте свой путь к файлу
            List<PhoneBookEntry> phoneBook = new List<PhoneBookEntry>();
            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines("data.txt");
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // пропускаем пустые строки

                    // Разбиваем строку по ;
                    string[] parts = line.Split(';');

                    // parts[0] - тип записи: Person / Organization / Friend
                    string entryType = parts[0].Trim();

                    switch (entryType.ToLower())
                    {
                        case "person":
                            if (parts.Length >= 4)
                            {
                                // Person;фамилия;адрес;телефон
                                string lastName = parts[1];
                                string address = parts[2];
                                string phone = parts[3];
                                phoneBook.Add(new Person(lastName, address, phone));
                            }
                            break;

                        case "organization":
                            if (parts.Length >= 6)
                            {
                                // Organization;название;адрес;телефон;факс;контактное_лицо
                                string orgName = parts[1];
                                string address = parts[2];
                                string phone = parts[3];
                                string fax = parts[4];
                                string contactPerson = parts[5];
                                phoneBook.Add(new Organization(orgName, address, phone, fax, contactPerson));
                            }
                            break;

                        case "friend":
                            if (parts.Length >= 5)
                            {
                                // Friend;фамилия;адрес;телефон;дата рождения (yyyy-MM-dd или др. формат)
                                string lastName = parts[1];
                                string address = parts[2];
                                string phone = parts[3];
                                DateTime birthday;
                                if (DateTime.TryParse(parts[4], out birthday))
                                {
                                    phoneBook.Add(new Friend(lastName, address, phone, birthday));
                                }
                            }
                            break;

                        default:
                            Console.WriteLine($"Неизвестный тип записи: {entryType}");
                            break;
                    }
                }
            } 
            else
            {
                using (FileStream f = new FileStream("input.dat", FileMode.OpenOrCreate))
                {
                    phoneBook = (List<PhoneBookEntry>)formatter.Deserialize(f);
                    Console.WriteLine("Десериализация: ");
                }
                
            }

            phoneBook.Add(new Person("asd","qwer","879564"));
           
            Console.ReadLine();
            // Выводим полную информацию из базы на экран
            Console.WriteLine("Полная информация обо всех записях:");
            foreach (var entry in phoneBook)
            {
                entry.PrintInfo();
            }
            Console.ReadLine();
            // Организуем поиск по фамилии
            Console.WriteLine("Введите фамилию для поиска:");
            string searchLastName = Console.ReadLine();

            Console.WriteLine($"Результаты поиска по фамилии \"{searchLastName}\":");
            bool found = false;
            foreach (var entry in phoneBook)
            {
                if (entry.MatchesCriteria(searchLastName))
                {
                    entry.PrintInfo();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Записей с такой фамилией не найдено.");
            }
            using (FileStream f = new FileStream("input.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(f, phoneBook);
            }
            Console.ReadLine();

        }
    }
}
