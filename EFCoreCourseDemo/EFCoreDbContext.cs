using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        public DbSet<User> Users { get; set; }

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


            #region 枚举映射 (TinyInt)
            //modelBuilder.Entity<Blog>(b =>
            //{
            //    b.Property(p => p.Categorys).HasColumnType("TINYINT");
            //});
            #endregion

            #region 枚举映射（String）方式一  有中文的情况
            //modelBuilder.Entity<Blog>(b =>
            //{
            //    //HasConversion 字符串 值转化器
            //    b.Property(p => p.Categorys).HasConversion(typeof(string)).HasMaxLength(20);//typeof(string)内置了将枚举转化成字符串的转化器
            //});
            #endregion

            #region 枚举映射（string）方式二  没有中文的情况（SqlServer中有 VARCHAR NVARCHAR的区分）
            //modelBuilder.Entity<Blog>(b =>
            //{
            //    b.Property(p => p.Categorys).HasColumnType("VARCHAR(20)");
            //});
            #endregion

            #region 值转化器(密码加密)
            //modelBuilder.Entity<User>(b =>
            //{
            //    b.Property(p => p.Password).HasConversion(v=> Encrypt(v),v=>Decrypt(v));
            //});
            #endregion

            #region 值转化器（转化String）
            modelBuilder.Entity<Blog>(b =>
            {
                //IsUnicode(false) 表示的是vchar 而不是nvarchar  (具体可以在Sql Server中体现)
                //b.Property(p => p.boolConvertChar).HasMaxLength(1).IsUnicode(false)
                //.HasConversion(typeof(string));

                //方法一
                //var boolCharConverter = new ValueConverter<bool, string>(x => x ? "X" : "Y", y => y.Equals("X"));
                //b.Property(p => p.boolConvertChar).HasMaxLength(1).IsUnicode(false)
                //.HasConversion(boolCharConverter);

                //方法二  通过委托来实现的
                b.Property(p => p.boolConvertChar).HasMaxLength(1).IsUnicode(false)
                .HasConversion(x => x ? "X" : "Y", y => y.Equals("X"));
            });
            #endregion

            #region MyRegion
            //不能转化成null,也就是当配置映射字段是不为null的时候,我们在添加数据的时候不去
            //添加Name的时候 ToString() 会变成null,会报错
            modelBuilder.Entity<Blog>(b =>
            {
                b.Property(p => p.Name).IsRequired().HasConversion(v=>v.ToString(),v=>v);
            });
            #endregion

            #endregion



            Console.WriteLine("**********Blog表开始初始化数据**********");
        }

        #region 加密算法+static string Encrypt(string password)
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <returns></returns>
        public static string Encrypt(string password)
        {
            return password + "qwer";
        }
        #endregion

        #region 解密算法+static string Decrypt(string password)
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="password">加密后的密码</param>
        /// <returns></returns>
        public static string Decrypt(string password)
        {
            return password.Remove(0,password.Length-4);
        }
        #endregion

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
