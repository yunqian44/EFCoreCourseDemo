using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Linq;

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
            #region 数据库数据映射
            //modelBuilder.Entity<Blog>().HasData(
            //       //new Blog{Id=1,Name="DDD领域驱动模型"},
            //       new Blog { Id = 2, Name = "EntityFramework Core 3.1.1" });
            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    Id = 1,
            //    BlogId = 1,
            //    Title = "技术交流",
            //    Content = "第一次写博客，先随便写一下"
            //}); 
            #endregion

            #region 数据库表映射
            //modelBuilder.Entity<Blog>().ToTable("Blog");//这时表明就变成Blog

            //EF Core中表名最大长度是128
            //var tableName = string.Join("", Enumerable.Repeat("t", 250).ToArray());//生成250个t的字符串
            //modelBuilder.Entity<Blog>().ToTable(tableName);

            #endregion

            #region 数据库表字段映射


            #region 更新时间方式一(使用结算列) （SqlServer可用）
            //modelBuilder.Entity<Blog>(b =>
            //    {
            //        b.Property(p => p.CreatedTime)
            //            .HasColumnType("datetime")
            //            .HasDefaultValueSql("now()");

            //        b.Property(p => p.UpdatedTime)
            //            .HasColumnType("datetime")
            //            .HasComputedColumnSql("now()");//使用结算列解决（SqlServer）
            //});
            #endregion


            #region 更新时间方式二(重写SaveChanges()方法) MySql 可用
            //modelBuilder.Entity<Blog>(b =>
            //    {
            //        b.Property(p => p.CreatedTime)
            //            .HasColumnType("datetime")
            //            .HasDefaultValueSql("now()");

            //        b.Property(p => p.UpdatedTime)
            //            .HasColumnType("datetime")
            //            .HasDefaultValueSql("now()");
            //    });
            #endregion


            #region 枚举映射
            modelBuilder.Entity<Blog>(b =>
            {
                b.Property(p => p.Categorys).HasColumnType("TINYINT");
            });
            #endregion

            #endregion



            Console.WriteLine("**********Blog表开始初始化数据**********");
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().ToList();
            var updatedEntires = entries.Where(e => e.Entity is IUpdatedable
             && e.State == EntityState.Modified).ToList();

            updatedEntires.ForEach(e =>
            {
                ((IUpdatedable)e.Entity).UpdatedTime = DateTime.Now;
            });


            return base.SaveChanges();

        }
    }
}
