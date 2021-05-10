using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public readonly struct Fraction
{
    private readonly int num;
    private readonly int den;

    public Fraction(int numerator, int denominator)
    {
        switch (denominator)
        {
            case 0:
                throw new ArgumentException("Ноль нельзя!");
            case int n when n < 0:
                numerator *= -1;
                denominator *= -1;
                break;
        }
        var gcd = FindGcd(Math.Abs(numerator), denominator);
        num = numerator / gcd;
        den = denominator / gcd;
    }

    public static int FindGcd(int a, int b)
    {
        while (b != 0)
        {
            a %= b;
            a ^= b;
            b ^= a;
            a ^= b;
        }

        return a + b;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        var num = a.num * b.den + b.num * a.den;
        var den = a.den * b.den;
        return new Fraction(num, den);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        var num = a.num * b.den - b.num * a.den;
        var den = a.den * b.den;
        return new Fraction(num, den);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        var num = a.num * b.num;
        var den = a.den * b.den;
        return new Fraction(num, den);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.den == 0)
            throw new DivideByZeroException("Ноль нельзя!");
        var num = a.num * b.den;
        var den = a.den * b.num;
        return new Fraction(num, den);
    }

    public override string ToString()
    {
        if (num == 0 || den == 1) return $"{num}";
        return $"{num}/{den}";
    }
}

public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            var numden = Console.ReadLine().Split('/');
            var frac1 = new Fraction(int.Parse(numden[0]), int.Parse(numden[1]));
            numden = Console.ReadLine().Split('/');
            var frac2 = new Fraction(int.Parse(numden[0]), int.Parse(numden[1]));
            Console.WriteLine(frac1 + frac2);
            Console.WriteLine(frac1 - frac2);
            Console.WriteLine(frac1 * frac2);
            Console.WriteLine(frac1 / frac2);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
    }
}
