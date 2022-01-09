using Microsoft.EntityFrameworkCore;

namespace ControleEstoque2.Models
{
    public class EstoqueWebContext : DbContext
    {
        public EstoqueWebContext(DbContextOptions<EstoqueWebContext> options) : base(options)
        {
        }

        public DbSet<CategoriaModel> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
          modelBuilder.Entity<CategoriaModel>().ToTable("Categoria"); //Definindo o nome da tabela no banco
      }
    }
}