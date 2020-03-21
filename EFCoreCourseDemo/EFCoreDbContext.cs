using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;

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
        }
    }
}
