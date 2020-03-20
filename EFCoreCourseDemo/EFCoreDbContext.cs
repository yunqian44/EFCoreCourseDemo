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
            .UseSqlServer(@"Server=.;Database=EFCore;Trusted_Connection=True;");

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
