using System;
using System.Globalization;
using System.Threading;

/*
Источник: https://metanit.com/

Класс Celcius представляет градусник по Цельсию, а Fahrenheit - градусник по Фаренгейту.
Определите операторы преобразования от типа Celcius и наоборот.
Преобразование температуры по шкале Фаренгейта (Tf) в температуру по шкале Цельсия (Tc): Tc = 5/9 * (Tf - 32).
Преобразование температуры по шкале Цельсия в температуру по шкале Фаренгейта: Tf = 9/5 * Tc + 32.

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки - количество градусов в Фаренгейтах и количество градусов в Цельсиях.
50
50
Программа должна вывести на экран число градусов в Цельсиях и Фаренгейтах, соответственно
с использованием перегруженных операторов (округлять до 2 знаков после запятой):
10,00
122,00

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но celвывод меняться не должен.
*/

namespace Task04
{
    class Celcius
    {
        private const double ToFahrenheitMult = 9.0 / 5.0;
        private const double ToFahrenheitAdd = 32;
        public double Gradus { get; set; }

        public static explicit operator Fahrenheit(Celcius celcius)
        {
            return new Fahrenheit {Gradus = ToFahrenheitMult * celcius.Gradus + ToFahrenheitAdd};
        }

        public override string ToString()
        {
            return $"{Gradus:F2}";
        }
    }

    class Fahrenheit
    {
        private const double ToCelsiusMult = 5.0 / 9.0;
        private const double ToCelciusAdd = 32;
        public double Gradus { get; set; }

        public static explicit operator Celcius(Fahrenheit fahrenheit)
        {
            return new Celcius {Gradus = ToCelsiusMult * (fahrenheit.Gradus - ToCelciusAdd)};
        }

        public override string ToString()
        {
            return $"{Gradus:F2}";
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            var fahrenheit = new Fahrenheit {Gradus = double.Parse(Console.ReadLine())};
            var celcius = new Celcius {Gradus = double.Parse(Console.ReadLine())};
            Console.WriteLine((Celcius)fahrenheit);
            Console.WriteLine((Fahrenheit)celcius);
        }
    }
}