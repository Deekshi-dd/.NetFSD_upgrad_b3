using System;

class Node
{
    public int EmpId;
    public string Name;
    public Node Next;

    public Node(int id, string name)
    {
        EmpId = id;
        Name = name;
        Next = null;
    }
}

class EmployeeLinkedList
{
    private Node head;

  
    public void InsertAtBeginning(int id, string name)
    {
        Node newNode = new Node(id, name);
        newNode.Next = head;
        head = newNode;
    }

  
    public void InsertAtEnd(int id, string name)
    {
        Node newNode = new Node(id, name);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Node temp = head;
        while (temp.Next != null)
        {
            temp = temp.Next;
        }

        temp.Next = newNode;
    }

    public void Delete(int id)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

      
        if (head.EmpId == id)
        {
            head = head.Next;
            return;
        }

        Node temp = head;
        Node prev = null;

        while (temp != null && temp.EmpId != id)
        {
            prev = temp;
            temp = temp.Next;
        }

        if (temp == null)
        {
            Console.WriteLine("Employee not found");
            return;
        }

        prev.Next = temp.Next;
    }

    public void Display()
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        Node temp = head;
        while (temp != null)
        {
            Console.WriteLine(temp.EmpId + " - " + temp.Name);
            temp = temp.Next;
        }
    }
}

class Program
{
    static void Main()
    {
        EmployeeLinkedList list = new EmployeeLinkedList();

    
        list.InsertAtEnd(101, "John");
        list.InsertAtEnd(102, "Sara");
        list.InsertAtEnd(103, "Mike");

        list.Delete(102)
        Console.WriteLine("Employee List After Deletion:");
        list.Display();
    }
}
