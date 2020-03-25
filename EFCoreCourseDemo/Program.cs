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
            var context = new EFCoreDbContext();
            context.Database.EnsureDeleted();
            Console.WriteLine("**********开始初始化数据**********");
            context.Database.EnsureCreated();

            #region Data Seeding 插入操作 更新操作
            //context.Add<Blog>(new Blog { Name = "ef core" });

            //var blog = context.Blogs.Find(1);//查询主键为1
            //blog.Name = "EntityFramework Core 5";
            //context.SaveChanges(); 
            #endregion


            #region 枚举类型在EF映射 在Net Core 2.0查询的时候会报错现在ok
            //如果枚举没有给定值 默认给定的是0 也就是字符串0 这个时候得处理了  可以在属性上默认
            //context.Add<Blog>(new Blog { Name = "ef core",Categorys= Category.Technology });
            //context.SaveChanges();
            //var blog = context.Blogs.FirstOrDefault(d=>d.Categorys== Category.Technology);//查询主键为1
            #endregion

            #region 值转化器 (转化方法)

            //假设密码是明文的，暂时不要去纠结
            //context.Users.Add(new User {Name="张三",Password="123" });
            //context.SaveChanges();
            #endregion

            #region 值转化器 (bool转String)

            //true 映射到数据库的话是一个为1的字符串
            //context.Blogs.Add(new Blog { Name = "张三", boolConvertChar=true });
            //context.SaveChanges();
            #endregion

            #region 值转化器（实现扩展属性）
            context.Users.Add(new User
            {
                Name = "张三",
                BirthDate = new BirthDate(1993, 03, 07)
            });
            context.SaveChanges();

            foreach (var user in context.Users.ToList())
            {
                Console.WriteLine($"距离我下一次生日还有[{user.BirthDate.DaysOfNextBirthday()}]天");
            }
            #endregion

            Console.WriteLine("**********初始化数据完成**********");

            #endregion

            #region 控制台程序迁移的方式  添加，修改使用的方式
            //var context = new EFCoreDbContext();
            //Console.WriteLine("**********开始初始化数据**********");
            //context.Database.Migrate();
            //Console.WriteLine("**********初始化数据完成**********");
            #endregion

            #region HasData() 自定义初始化逻辑
            //using (var ctx=new EFCoreDbContext())
            //{
            //    ctx.Database.EnsureCreated();
            //    var testBlog = ctx.Blogs.FirstOrDefault(d => d.Name.Equals("微服务设计理念"));
            //    if (testBlog==null)
            //    {
            //        ctx.Blogs.Add(new Blog { Name = "微服务设计理念" });
            //    }
            //    ctx.SaveChanges();
            //}
            #endregion

            #region 使用命令提示框进行数据迁移
            /*使用命令PowerShell 命令行进行数据迁移
               1，dotnet tool install--global dotnet-ef  先安装

                  dotnet ef migrations add initial(迁移类的名称)  初始化
                  dotnet ef database update 更新数据库

                  dotnet ef migrations add update 更新迁移操作
                  dotnet ef  database update  更新数据库

                  dotnet ef migrations add delete   删除迁移操作
                  dotnet ef database update 更新数据库
                  */
            #endregion

            Console.ReadKey();
        }
    }
}
