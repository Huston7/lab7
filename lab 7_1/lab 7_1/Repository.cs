using System;
using System.Collections.Generic;

public class Repository<T>
{
    private List<T> items = new List<T>();

    public delegate bool Criteria<T>(T item);

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> Find(Criteria<T> criteria)
    {
        List<T> result = new List<T>();
        foreach (T item in items)
        {
            if (criteria(item))
            {
                result.Add(item);
            }
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);
        intRepository.Add(4);
        intRepository.Add(5);

        // Визначення критерію для пошуку парних чисел.
        Repository<int>.Criteria<int> evenCriteria = x => x % 2 == 0;
        List<int> evenNumbers = intRepository.Find(evenCriteria);

        Console.WriteLine("Парні числа:");
        foreach (int number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        Repository<string> stringRepository = new Repository<string>();
        stringRepository.Add("apple");
        stringRepository.Add("banana");
        stringRepository.Add("cherry");
        stringRepository.Add("date");
        stringRepository.Add("grape");

        // Визначення критерію для пошуку слов, що починаються з "a".
        Repository<string>.Criteria<string> startsWithACriteria = s => s.StartsWith("a", StringComparison.OrdinalIgnoreCase);
        List<string> aWords = stringRepository.Find(startsWithACriteria);

        Console.WriteLine("Слова, що починаються з 'a':");
        foreach (string word in aWords)
        {
            Console.WriteLine(word);
        }
    }
}