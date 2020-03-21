using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;

namespace EFCoreCourseDemo
{

    public class EFCoreDbContext : DbContext
    {
        //  public static readonly LoggerFactory loggerFactory
        //= new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public static readonly ILoggerFactory loggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLoggerFactory(loggerFactory)
            .UseMySql(@"server=127.0.0.1;uid=root;pwd=qwer1234!;database=EFCore;Character Set = utf8;");


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //针对于HasData限制（即使主键是由数据库生成的自动增长），也需要指定主键 

            //调用EnsureCreated方法只针对与添加数据有效，但是数据库如果有数据的话，
            //也就是对数据更改将无效
            Console.WriteLine("**********Blog表开始初始化数据**********");
            modelBuilder.Entity<Blog>().HasData(
                //new Blog{Id=1,Name="DDD领域驱动模型"},
                new Blog{Id = 2,Name = "EntityFramework Core 3.1.1"});
            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 1,
                BlogId=1,
                Title="技术交流",
                Content="第一次写博客，先随便写一下"
            });

           Console.WriteLine("**********Blog表开始初始化数据**********");
        }
    }
}
