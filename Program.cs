using System;
using System.Collections.Generic;
using app.Models;

namespace app
{
    public class Program
    {
        private string[,] _data = new string[,]
        {
            {
                "red",
                "gray",
                "pink",
                "orange"
            },
            {
                "orange",
                "sky",
                "gray",
                "pink"
            },
            {
                "mint",
                "orange",
                "purple",
                "mint"
            },
            {
                "purple",
                "sky",
                "red",
                "purple"
            },
            {
                "sky",
                "red",
                "gray",
                "mint"
            },
            {
                "purple",
                "pink",
                "pink",
                "orange"
            },
            {
                "sky",
                "mint",
                "red",
                "gray"
            },
            {
                "",
                "",
                "",
                ""
            },
            {
                "",
                "",
                "",
                ""
            }
        };

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            RootAnalyzer analyzer = new RootAnalyzer();
            List<RootSelection> result = analyzer.Analyze(_data);
            Console.WriteLine("result:");
            foreach (RootSelection selection in result)
            {
                Console.WriteLine($"{selection.From + 1} to {selection.To + 1}");
            }
        }
    }
}
