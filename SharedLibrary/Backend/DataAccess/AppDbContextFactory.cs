using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedLibrary.Backend.BusinessLogic;

namespace SharedLibrary.Backend.DataAccess
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=/Users/kristinacortsen/RiderProjects/registration-system/SharedLibrary/database.db"); 

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}