using Microsoft.EntityFrameworkCore;
using PostgreSQL_DotNet_Core.Models;
using System.Collections.Generic;

namespace PostgreSQL_DotNet_Core.DBContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       public DbSet<accgeninf> accgeninf { get; set; }
    }
}
