using System;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var p = new Parser();
                Console.WriteLine("Enter path and .ini name:");
                //"/Users/ant0xak/RiderProjects/lab1/project/text.ini"
                var file = Console.ReadLine();
                p.ReadInfo(file);
                Console.WriteLine("Enter section name:");
                var section = Console.ReadLine();
                Console.WriteLine("Enter value's name:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter value's type ( int, double, string ) :");
                var type = Console.ReadLine();
                switch (type)
                {
                    case "string":
                        Console.WriteLine(p.GetValue<string>(section, name, type));
                        break;
                    case "int":
                        Console.WriteLine(p.GetValue<int>(section, name, type));
                        break;
                    case "double":
                        Console.WriteLine(p.GetValue<double>(section, name, type));
                        break;
                    
                    default:
                        throw new Exception("No value with such type: " + type);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}