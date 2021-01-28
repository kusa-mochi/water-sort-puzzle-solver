using System;
using app.Models;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Capsule capsule = new Capsule(new string[]{ "緑", "赤", "赤", "青" });
        }
    }
}
