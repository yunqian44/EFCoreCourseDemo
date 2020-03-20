using System;

namespace EFCoreCourseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EFCoreDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.ReadKey();
        }
    }
}
