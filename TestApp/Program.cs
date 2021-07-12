using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine($"Usage: app.exe file.csv");
                return;
            }

            var fileName = args[0];
            IEnumerable<StateRecord> stateRecords;
            try
            {
                stateRecords = new DataReader().ReadoutRecords(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to read file: {e}");
                return;
            }

            var reportManager = new ReportManager(stateRecords);

            try
            {
                Console.WriteLine("Report1:");
                foreach (var (date, maxCount) in reportManager.Report1())
                {
                    Console.WriteLine($"{date.ToShortDateString()} {maxCount}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to build Report1: {e}");
                return;
            }

            Console.WriteLine();

            try
            {
                Console.WriteLine("Report2:");
                foreach (var (operatorName, statesDict) in reportManager.Report2())
                {
                    Console.WriteLine(
                        operatorName + " " +
                        string.Join(" ", statesDict
                            .Select(kv => kv.Key + " " + $"{kv.Value:0.##}")));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to build Report2: {e}");
                return;
            }
        }
    }
}