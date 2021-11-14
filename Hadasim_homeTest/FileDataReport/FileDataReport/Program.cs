using System;
using System.IO;

namespace FileDataReport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The report:");
            Read read = new Read();
            read.Reporting();

        }
    }
}
