using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority>
{
    private readonly PriorityQueue<TTask, TPriority> taskQueue = new PriorityQueue<TTask, TPriority>();
    private readonly Func<TTask> objectInitializer;
    private readonly Action<TTask> objectReset;

    public delegate void TaskExecution(TTask task);

    public TaskScheduler(Func<TTask> initializer, Action<TTask> reset)
    {
        objectInitializer = initializer;
        objectReset = reset;
    }

    public void EnqueueTask(TTask task, TPriority priority)
    {
        taskQueue.Enqueue(task, priority);
    }

    public void ExecuteNext(TaskExecution execution)
    {
        if (taskQueue.Count > 0)
        {
            var task = taskQueue.Dequeue();
            execution(task);
        }
    }

    public TTask InitializeObject()
    {
        return objectInitializer();
    }

    public void ResetObject(TTask task)
    {
        objectReset(task);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Приклад використання TaskScheduler для роботи з завданнями типу int і пріоритетами string.

        TaskScheduler<int, string> taskScheduler = new TaskScheduler<int, string>(
            initializer: () => 0,
            reset: task => { /* Нічого не робимо для скидання int */ });

        taskScheduler.EnqueueTask(1, "High");
        taskScheduler.EnqueueTask(2, "Low");
        taskScheduler.EnqueueTask(3, "Medium");

        while (true)
        {
            taskScheduler.ExecuteNext(task => Console.WriteLine($"Виконуємо завдання: {task}"));
            Console.WriteLine("Введіть нове завдання (Enter - завершити):");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                break;
            int newTask = int.Parse(input);
            string priority = "High"; // Встановіть відповідний пріоритет за потребою.
            taskScheduler.EnqueueTask(newTask, priority);
        }
    }
}