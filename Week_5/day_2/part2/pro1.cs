using System;

class UndoStack
{
    private string[] stack;
    private int top;
    private int capacity;

    public UndoStack(int size)
    {
        capacity = size;
        stack = new string[capacity];
        top = -1;
    }

    // Push operation (Add action)
    public void Push(string action)
    {
        if (top == capacity - 1)
        {
            Console.WriteLine("Stack Overflow! Cannot add more actions.");
            return;
        }

        stack[++top] = action;
        Console.WriteLine($"Action performed: {action}");
        Display();
    }

    // Pop operation (Undo action)
    public void Pop()
    {
        if (top == -1)
        {
            Console.WriteLine("Stack Underflow! No actions to undo.");
            return;
        }

        Console.WriteLine($"Undo action: {stack[top]}");
        top--;
        Display();
    }

    // Display current state
    public void Display()
    {
        if (top == -1)
        {
            Console.WriteLine("Current State: Empty");
            return;
        }

        Console.Write("Current State: ");
        for (int i = 0; i <= top; i++)
        {
            Console.Write(stack[i]);
            if (i < top)
                Console.Write(" -> ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        UndoStack undoStack = new UndoStack(10);

        // Sample Operations
        undoStack.Push("Type A");
        undoStack.Push("Type B");
        undoStack.Push("Type C");

        undoStack.Pop(); // Undo C
        undoStack.Pop(); // Undo B
    }
}
