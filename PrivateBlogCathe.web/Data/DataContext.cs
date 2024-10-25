using Microsoft.EntityFrameworkCore;
using PrivateBlogCathe.web.Data.Entities;

namespace PrivateBlogCathe.web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {           
        }

        public DbSet<Section> Sections { get; set; }
    }
}
