using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreCourseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 控制台程序迁移的方式  初始化使用的方式
            var context = new EFCoreDbContext();
            context.Database.EnsureDeleted();
            Console.WriteLine("**********开始初始化数据**********");
            context.Database.EnsureCreated();
            Console.WriteLine("**********初始化数据完成**********");
            #endregion


            #region 控制台程序迁移的方式  添加，修改使用的方式
            Console.WriteLine("**********开始初始化数据**********");
            context.Database.Migrate();
            Console.WriteLine("**********初始化数据完成**********"); 
            #endregion

            Console.ReadKey();
        }
    }
}
