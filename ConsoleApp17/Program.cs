using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DemoMyString
{
    /// <summary>
    /// Класс для работы с неизменяемыми строками
    /// </summary>
    public class MyString
    {
        // 1. Закрытое поле:
        private string line;

        // 2. Конструкторы

        /// <summary>
        /// Конструктор, создающий строку заданной длины (по умолчанию заполненную пробелами)
        /// </summary>
        /// <param name="length">Длина строки</param>
        public MyString(int length)
        {
            // Допустим, создаём строку из length пробелов
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Длина не может быть отрицательной.");

            line = new string(' ', length);
        }

        /// <summary>
        /// Конструктор на основе заданного строкового литерала
        /// </summary>
        /// <param name="str">Исходная строка</param>
        public MyString(string str)
        {
            // Если str == null, то можно принять решение хранить пустую строку или выбросить исключение.
            // Здесь для удобства принимаем, что null - это пустая строка.
            line = str ?? string.Empty;
        }

        /// <summary>
        /// Конструктор копирования (на основе существующего экземпляра класса)
        /// </summary>
        /// <param name="other">Другой объект MyString</param>
        public MyString(MyString other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Нельзя копировать из null-ссылки.");

            line = other.line;
        }

        // 3. Методы

        /// <summary>
        /// Подсчитать количество цифр в строке
        /// </summary>
        /// <returns>Число символов, являющихся цифрами</returns>
        public int CountDigits()
        {
            return line.Count(char.IsDigit);
        }

        /// <summary>
        /// Подсчитать сумму цифр в строке
        /// </summary>
        /// <returns>Сумма всех цифр, содержащихся в строке</returns>
        public int SumDigits()
        {
            // Каждая цифра '0'..'9' при преобразовании в int даст 48..57, но для вычитания смещения '0' используем: (c - '0')
            return line.Where(char.IsDigit).Sum(c => c - '0');
        }

        // 4. Перегрузка методов предка Object

        /// <summary>
        /// Переопределение ToString, возвращающее саму строку
        /// </summary>
        /// <returns>Значение внутренней строки</returns>
        public override string ToString()
        {
            return line;
        }

        /// <summary>
        /// Переопределение GetHashCode
        /// </summary>
        /// <returns>Хеш-код строки</returns>
        public override int GetHashCode()
        {
            return line.GetHashCode();
        }

        /// <summary>
        /// Переопределение Equals
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>true, если равны, иначе false</returns>
        public override bool Equals(object obj)
        {
            if (obj is MyString other)
            {
                return line == other.line;
            }
            return false;
        }

        /// <summary>
        /// В C# метод GetType() является невиртуальным и не подлежит переопределению.
        /// Формально мы можем лишь вызвать базовый GetType(), 
        /// но переопределить его нельзя. Оставим для демонстрации.
        /// </summary>
        public new Type GetType()
        {
            // Можно вернуть базовый тип (this.GetType()), но обычно это не переопределяется.
            return base.GetType();
        }

        // 5. Свойства

        /// <summary>
        /// Свойство для доступа к внутренней строке (доступное для чтения и записи)
        /// При записи можно сохранять неизменяемость, но в задании сказано "доступно для записи",
        /// значит допускаем изменить поле через это свойство.
        /// </summary>
        public string Line
        {
            get => line;
            set => line = value ?? string.Empty; // не даём записать null
        }

        /// <summary>
        /// Свойство, возвращающее общее количество символов в строке (только для чтения)
        /// </summary>
        public int Length
        {
            get => line.Length;
        }

        // 6. Индексатор, позволяющий получать символ строки по индексу (только чтение)
        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= line.Length)
                    throw new IndexOutOfRangeException("Недопустимый индекс символа.");
                return line[index];
            }
        }

        // 7. Перегрузка операторов

        /// <summary>
        /// Унарный оператор !:
        /// Возвращает true, если строка не пустая, иначе false.
        /// (Необычно, т.к. ! обычно означает логическое отрицание, но делаем по условию)
        /// </summary>
        /// <param name="s">Объект MyString</param>
        /// <returns>true, если строка не пустая, иначе false</returns>
        public static bool operator !(MyString s)
        {
            // Если s == null, примем, что строка как бы пуста
            if (s is null) return false;
            return s.line.Length != 0;
        }

        /// <summary>
        /// Операторы true/false:
        /// - true, если строка является палиндромом (без учёта регистра)
        /// - false, если не палиндром
        /// 
        /// В конструкции if (myString) { ... } будет вызываться operator true(MyString).
        /// Для корректной работы требуется определить оба оператора (true и false).
        /// </summary>
        public static bool operator true(MyString s)
        {
            if (s is null) return false;

            // Проверяем, является ли строка палиндромом (без учёта регистра)
            // Пример простейшей проверки:
            string lower = s.line.ToLower();
            string rev = new string(lower.Reverse().ToArray());
            return lower == rev;
        }

        public static bool operator false(MyString s)
        {
            if (s is null) return false;

            // Проверяем, является ли строка палиндромом (без учёта регистра)
            // Пример простейшей проверки:
            string lower = s.line.ToLower();
            string rev = new string(lower.Reverse().ToArray());
            return lower != rev;
        }

        /// <summary>
        /// Оператор &:
        /// Возвращает true, если строковые поля двух объектов посимвольно равны (без учёта регистра),
        /// иначе false.
        /// </summary>
        public static bool operator &(MyString a, MyString b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return string.Equals(a.line, b.line, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Явное преобразование MyString -> string
        /// </summary>
        public static explicit operator string(MyString s)
        {
            return s?.line ?? string.Empty;
        }

        /// <summary>
        /// Явное преобразование string -> MyString
        /// </summary>
        public static explicit operator MyString(string s)
        {
            return new MyString(s);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Для демонстрации:
            // 1) Считаем строки из входного файла "input.txt"
            // 2) Создадим объекты MyString на каждой строке
            // 3) Сохраним в коллекции List<MyString>
            // 4) Выполним некоторые операции
            // 5) Запишем результаты в выходной файл "output.txt"

            string inputFile = "input.txt";
            string outputFile = "output.txt";

            // Создаём список для хранения экземпляров нашего класса
            List<MyString> myStrings = new List<MyString>();

            // 1) Считаем строки из входного файла (если файла нет, можно создать вручную)
            if (File.Exists(inputFile))
            {
                var lines = File.ReadAllLines(inputFile);

                // 2) Создадим объекты MyString из каждой строки
                foreach (var line in lines)
                {
                    MyString ms = new MyString(line);
                    myStrings.Add(ms);
                }
            }
            else
            {
                Console.WriteLine($"Файл {inputFile} не найден. Будет создан примерный список строк.");
                // Создадим примерный список, если файла нет
                myStrings.Add(new MyString("12345"));
                myStrings.Add(new MyString("RaceCar"));
                myStrings.Add(new MyString(""));
                myStrings.Add(new MyString("test"));
                myStrings.Add(new MyString("999"));
            }

            // 4) Выполним некоторые операции и выведем результат на консоль
            //    Параллельно сформируем список строк для записи в файл.

            List<string> outputLines = new List<string>();

            foreach (MyString ms in myStrings)
            {
                int digitsCount = ms.CountDigits();
                int digitsSum = ms.SumDigits();

                // Проверка палиндрома с помощью операторов true/false:
                bool isPalindrome = (ms ? true : false); // или просто: if (ms) ...

                // Проверка, не пустая ли строка через оператор !
                bool notEmptyViaExclamation = !ms;
                // по условию: "!": true, если НЕ пустая строка. 
                // Это может показаться противоречивым названию, но так сказано в задании.

                // Сконвертируем объект в обычную string
                string asString = (string)ms; // явное приведение

                // Формируем текстовый результат
                string info = $"Исходная строка: \"{asString}\"; " +
                              $"Кол-во цифр: {digitsCount}; " +
                              $"Сумма цифр: {digitsSum}; " +
                              $"Палиндром? {isPalindrome}; " +
                              $"!ms => {notEmptyViaExclamation}; " +
                              $"Длина: {ms.Length}.";

                Console.WriteLine(info);
                outputLines.Add(info);
            }

            // 5) Запишем результат в выходной файл
            File.WriteAllLines(outputFile, outputLines);

            // Дополнительно покажем, как работает оператор & для пары строк:
            if (myStrings.Count >= 2)
            {
                MyString first = myStrings[0];
                MyString second = myStrings[1];

                // Оператор &: равны ли строки посимвольно без учёта регистра?
                bool areEqualIgnoreCase = first & second;
                Console.WriteLine($"Сравнение (оператор &): \"{first}\" & \"{second}\" => {areEqualIgnoreCase}");
            }

            // Также покажем, как использовать конструктор копирования:
            if (myStrings.Count > 0)
            {
                MyString copy = new MyString(myStrings[0]);
                Console.WriteLine($"Сделали копию первого элемента: \"{copy}\" (Equals оригиналу? {copy.Equals(myStrings[0])})");
            }

            Console.WriteLine();
            Console.WriteLine("Демонстрация завершена. Результаты сохранены в файл " + outputFile);
            Console.ReadLine();
        }
    }
}