namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N;
            int even_count=0;
            int odd_count=0;
            int sum = 0;

            Console.Write("Enter Number: ");
            N = int.Parse(Console.ReadLine());

            for(int i = 1; i <= N;i++)
            {
                if (i %  2 == 0)
                {
                    even_count++;
                }
                else
                {
                    odd_count++;
                }
                sum += i;
            }

            Console.WriteLine($"Even Count: {even_count}");
            Console.WriteLine($"Odd Count: {odd_count}");
            Console.WriteLine($"Sum : {sum}");

        }
    }
}
