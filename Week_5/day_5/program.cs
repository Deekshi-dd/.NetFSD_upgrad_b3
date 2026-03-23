using System;
using System.IO;
using System.Text;
using System.Linq;

namespace assignmentDay25
{
    class Program
    {
        static string baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "Day25Work");

        static void Main(string[] args)
        {
          
            if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder);

            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("======= DAY 25 ASSIGNMENT MENU (MAC) =======");
                Console.WriteLine("1. Problem 1: Log Message Writer (FileStream)");
                Console.WriteLine("2. Problem 2: File Auditor (FileInfo)");
                Console.WriteLine("3. Problem 3: Performance Evaluator (Tuples)");
                Console.WriteLine("4. Problem 4: Directory Structure Analyzer");
                Console.WriteLine("5. Problem 5: Disk Monitor (DriveInfo)");
                Console.WriteLine("6. Exit");
                Console.Write("\nSelect a problem to run: ");
                
                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1: RunProblem1(); break;
                    case 2: RunProblem2(); break;
                    case 3: RunProblem3(); break;
                    case 4: RunProblem4(); break;
                    case 5: RunProblem5(); break;
                }
                
                if(choice != 6) {
                    Console.WriteLine("\nPress Enter to return to menu...");
                    Console.ReadLine();
                }

            } while (choice != 6);
        }

        // --- PROBLEM 1
        static void RunProblem1()
        {
            string filePath = Path.Combine(baseFolder, "log.txt");
            Console.Write("Enter log message: ");
            string msg = Console.ReadLine() + Environment.NewLine;

            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                fs.Write(data, 0, data.Length);
            }
            Console.WriteLine("Saved to: " + filePath);
        }

        // --- PROBLEM 2
        static void RunProblem2()
        {
            Console.WriteLine($"Auditing folder: {baseFolder}");
            DirectoryInfo di = new DirectoryInfo(baseFolder);
            FileInfo[] files = di.GetFiles();

            foreach (var f in files)
                Console.WriteLine($"- {f.Name} ({f.Length} bytes) Created: {f.CreationTime}");
            
            Console.WriteLine("Total Files: " + files.Length);
        }

        // --- PROBLEM 3
        static void RunProblem3()
        {
            Console.Write("Enter Monthly Sales: ");
            double sales = double.Parse(Console.ReadLine()!);
            Console.Write("Enter Rating (1-5): ");
            int rating = int.Parse(Console.ReadLine()!);

            // Tuple Return
            var result = (sales, rating);

            // Pattern Matching Switch Expression
            string status = result switch {
                ( >= 100000, >= 4) => "High Performer",
                ( >= 50000, >= 3) => "Average Performer",
                _ => "Needs Improvement"
            };

            Console.WriteLine($"Result: {status}");
        }

        // --- PROBLEM 4
        static void RunProblem4()
        {
        
            string root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            DirectoryInfo di = new DirectoryInfo(root);
          
            var dirs = di.GetDirectories().Take(5); 

            foreach (var d in dirs) {
                try {
                    Console.WriteLine($"Folder: {d.Name} | Files: {d.GetFiles().Length}");
                } catch { }
            }
        }

        // --- PROBLEM 5
        static void RunProblem5()
        {
            DriveInfo di = new DriveInfo("/"); 
            double freePercent = ((double)di.AvailableFreeSpace / di.TotalSize) * 100;

            Console.WriteLine($"Drive: {di.Name}");
            Console.WriteLine($"Free Space: {di.AvailableFreeSpace / 1073741824} GB");
            
            if (freePercent < 15) Console.WriteLine("!!! WARNING: LOW DISK SPACE !!!");
            else Console.WriteLine("Disk health: Good");
        }
    }
}
