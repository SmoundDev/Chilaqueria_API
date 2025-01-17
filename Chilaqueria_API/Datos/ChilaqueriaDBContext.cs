using Microsoft.EntityFrameworkCore;
using static Chilaqueria_API.Models.Chi_Prod_db_Models;

namespace Chilaqueria_API.Datos
{
    public class ChilaqueriaDBContext:DbContext
    {
        public ChilaqueriaDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Prod_Users> Prod_Users { get; set; }

    }
}
