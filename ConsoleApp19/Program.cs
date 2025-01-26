using System;
using System.Collections.Generic;
using System.IO;

// Пример помещаем в одноимённое пространство имён
namespace PhoneBookExample
{
    // 1) Абстрактный класс телефонного справочника
    // Реализуем интерфейс IComparable<PhoneBookEntry>,
    // чтобы можно было сортировать по номеру телефона
    public abstract class PhoneBookEntry : IComparable<PhoneBookEntry>
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        protected PhoneBookEntry(string address, string phoneNumber)
        {
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Абстрактный метод для вывода информации
        public abstract void PrintInfo();

        // Абстрактный метод для проверки соответствия записи поисковому критерию (фамилия)
        public abstract bool MatchesCriteria(string lastName);

        // Реализация CompareTo для сортировки по номеру телефона
        public int CompareTo(PhoneBookEntry other)
        {
            if (other == null) return 1;
            // Сравниваем номера как строки. Можно уточнить способ, например Ordinal или IgnoreCase.
            return string.Compare(PhoneNumber, other.PhoneNumber, StringComparison.OrdinalIgnoreCase);
        }
    }

    // 2) Класс "Персона"
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
            return LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Класс "Организация"
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
            // Организация обычно не ищется по фамилии
            return false;
        }
    }

    // Класс "Друг"
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
            // 3) Считываем данные из файла и формируем базу (список)
            string filePath = "data.txt"; // Укажите путь к файлу
            List<PhoneBookEntry> phoneBook = new List<PhoneBookEntry>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(';');
                    string entryType = parts[0].Trim().ToLower();

                    switch (entryType)
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
                                // Friend;фамилия;адрес;телефон;дата рождения
                                string lastName = parts[1];
                                string address = parts[2];
                                string phone = parts[3];
                                if (DateTime.TryParse(parts[4], out DateTime birthday))
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
                Console.WriteLine("Файл не найден. Проверьте путь к файлу!");
                return;
            }

            // Сортируем базу по номеру телефона (метод CompareTo в абстрактном классе)
            phoneBook.Sort();

            // Выводим полную информацию из базы на экран
            Console.WriteLine("Полная информация обо всех записях (отсортировано по номеру телефона):");
            foreach (var entry in phoneBook)
            {
                entry.PrintInfo();
            }

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

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}