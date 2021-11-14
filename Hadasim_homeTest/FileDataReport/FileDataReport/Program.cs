using System;
using System.IO;

namespace FileDataReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "";
            Console.WriteLine("Enter the file path");
            file = Console.ReadLine();
            Console.WriteLine("The report:");
            Read read = new Read(file);
            read.Reporting();

        }
    }
}
