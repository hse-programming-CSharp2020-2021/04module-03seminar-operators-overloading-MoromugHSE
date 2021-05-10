using System;
using System.Threading;
using System.Globalization;

/*
Источник: https://metanit.com/

Класс Dollar представляет сумму в долларах, а Euro - сумму в евро.

Определите операторы преобразования от типа Dollar в Euro и наоборот.
Допустим, 1 евро стоит 1,14 долларов. При этом один оператор должен подразумевать явное,
и один - неявное преобразование. Обработайте ситуации с отрицательными аргументами
(в этом случае должен быть выброшен ArgumentException).

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки - количество долларов и количество евро.
10
100
Программа должна вывести на экран количество евро и долларов, соответственно,
с использованием перегруженных операторов (округлять до 2 знаков после запятой):
8,77
114,00

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

namespace Task05
{
    abstract class Money
    {
        public decimal Sum { get; set; }

        public override string ToString()
        {
            return $"{Sum:F2}";
        }
    }


    class Dollar : Money
    {
        private const decimal FromEuro = 1.14m;

        public static implicit operator Dollar(Euro euro)
        {
            if (euro.Sum < 0)
                throw new ArgumentException("Плохая сумма!");
            return new Dollar { Sum = FromEuro * euro.Sum };
        }
    }


    class Euro : Money
    {
        private const decimal FromDollar = 1 / 1.14m;

        public static explicit operator Euro(Dollar dollar)
        {
            if (dollar.Sum < 0)
                throw new ArgumentException("Плохая сумма!");
            return new Euro { Sum = FromDollar * dollar.Sum };
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            try
            {
                var dollar = new Dollar { Sum = decimal.Parse(Console.ReadLine()) };
                var euro = new Euro { Sum = decimal.Parse(Console.ReadLine()) };
                Console.WriteLine((Euro)dollar);
                Console.WriteLine((Dollar)euro);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
        }
    }
}
