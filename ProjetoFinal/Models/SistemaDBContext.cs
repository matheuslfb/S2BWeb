using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoFinal.Models
{
    public class SistemaDBContext : DbContext
    {
        public SistemaDBContext() : base("SistemaDbContext")
 {
        }
        public DbSet<Reclamacao> Reclamacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}