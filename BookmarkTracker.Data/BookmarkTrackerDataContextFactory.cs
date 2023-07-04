using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkTracker.Data
{
    public class BookmarkTrackerDataContextFactory : IDesignTimeDbContextFactory<BookmarkTrackerDataContext>
    {
        public BookmarkTrackerDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}BookmarkTracker.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new BookmarkTrackerDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
