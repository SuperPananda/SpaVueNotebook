using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaVueNotebook.Model
{
    public class ApplicationDbContext: DbContext
    {
        readonly DbContextOptions<ApplicationDbContext> options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _options) : base(_options)
        {
            this.options = _options;
        }

        public virtual DbSet<Note> Notes { get; set; }

        //Для миграции
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (options == null)
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=VueCRUDApp; Trusted_Connection=true;");
        }
    }
}
