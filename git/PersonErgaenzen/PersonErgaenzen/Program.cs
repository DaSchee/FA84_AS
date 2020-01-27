using System;

namespace PersonErgänzen
{
    class Program
    {
        static void Main(string[] args)
        {
            Person seventeen = new Person("Hugo", new DateTime(2001, 8, 30));
            Person eighteen = new Person("Hugo", new DateTime(2001, 8, 29));
            Console.WriteLine(seventeen.IsAtleast(18));
            Console.WriteLine(eighteen.IsAtleast(18));
            Console.WriteLine(seventeen.GetAge());
            Console.WriteLine(eighteen.GetAge());
        }
    }
}
