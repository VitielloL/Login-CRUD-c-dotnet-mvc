using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Models;

namespace ProcessoSeletivo.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> _context) : base(_context)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
