using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;

namespace EFCoreCourseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 控制台程序迁移的方式  初始化使用的方式
            //var context = new EFCoreDbContext();
            //context.Database.EnsureDeleted();
            //Console.WriteLine("**********开始初始化数据**********");
            //context.Database.EnsureCreated();
            //Console.WriteLine("**********初始化数据完成**********");
            #endregion


            #region 控制台程序迁移的方式  添加，修改使用的方式
            //var context = new EFCoreDbContext();
            //Console.WriteLine("**********开始初始化数据**********");
            //context.Database.Migrate();
            //Console.WriteLine("**********初始化数据完成**********");
            #endregion

            #region HasData() 自定义初始化逻辑
            using (var ctx=new EFCoreDbContext())
            {
                ctx.Database.EnsureCreated();
                var testBlog = ctx.Blogs.FirstOrDefault(d => d.Name.Equals("微服务设计理念"));
                if (testBlog==null)
                {
                    ctx.Blogs.Add(new Blog { Name = "微服务设计理念" });
                }
                ctx.SaveChanges();
            }
            #endregion

            Console.ReadKey();
        }
    }
}
