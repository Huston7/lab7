using System;

public class Calculator<T>
{
    public delegate T ArithmeticOperation(T a, T b);

    public ArithmeticOperation Add { get; set; }
    public ArithmeticOperation Subtract { get; set; }
    public ArithmeticOperation Multiply { get; set; }
    public ArithmeticOperation Divide { get; set; }

    public Calculator()
    {
        // Ініціалізуємо делегати для арифметичних операцій.
        Add = (a, b) => (dynamic)a + b;
        Subtract = (a, b) => (dynamic)a - b;
        Multiply = (a, b) => (dynamic)a * b;
        Divide = (a, b) => (dynamic)a / b;
    }

    public T PerformOperation(T a, T b, ArithmeticOperation operation)
    {
        return operation(a, b);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator<int> intCalculator = new Calculator<int>();
        int resultInt = intCalculator.PerformOperation(5, 3, intCalculator.Add);
        Console.WriteLine($"Додавання (int): {resultInt}");

        Calculator<double> doubleCalculator = new Calculator<double>();
        double resultDouble = doubleCalculator.PerformOperation(5.0, 3.0, doubleCalculator.Divide);
        Console.WriteLine($"Ділення (double): {resultDouble}");

        Calculator<float> floatCalculator = new Calculator<float>();
        float resultFloat = floatCalculator.PerformOperation(5.0f, 3.0f, floatCalculator.Multiply);
        Console.WriteLine($"Множення (float): {resultFloat}");
    }
}

