using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> tasks = new List<string>();
        int choice;

        do
        {
            Console.WriteLine("\nTo-Do List Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Remove Task");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter task: ");
                    string task = Console.ReadLine()!;

                    if (!string.IsNullOrWhiteSpace(task))
                    {
                        tasks.Add(task);
                        Console.WriteLine("Task added!");
                    }
                    else
                    {
                        Console.WriteLine("Task cannot be empty.");
                    }
                    break;

                case 2:
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No tasks available.");
                    }
                    else
                    {
                        Console.WriteLine("Tasks:");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i]}");
                        }
                    }
                    break;

                case 3:
                    Console.Write("Enter task number to remove: ");

                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        if (index >= 1 && index <= tasks.Count)
                        {
                            Console.WriteLine("Removed: " + tasks[index - 1]);
                            tasks.RemoveAt(index - 1);
                        }
                        else
                        {
                            Console.WriteLine("Invalid task number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case 4:
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 4);
    }
}
