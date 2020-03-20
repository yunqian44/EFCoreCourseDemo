using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Data;
using System.Linq;

namespace EFCore.UnitTest
{

    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Blog> Blogs { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options= CreateDbContextOptions("EFCoreDemo");

            var context = new EFCoreDbContext(options);

            context.Blogs.Add(new Blog { Name = "ef core" });

            context.SaveChanges();

            var blog = context.Blogs.FirstOrDefault(d => d.Id == 1);

            Assert.IsNotNull(blog);
        }


        public static DbContextOptions<EFCoreDbContext> CreateDbContextOptions(string databaseName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EFCoreDbContext>();
            builder.UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

    }
}
