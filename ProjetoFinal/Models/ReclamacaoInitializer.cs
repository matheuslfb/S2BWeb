using ProjetoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoFinal.Models
{
    public class ReclamacaoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SistemaDBContext>
    {
        protected override void Seed(SistemaDBContext context)
        {
            var cat = new List<Categoria>
            {
                new Categoria { Titulo = "Água e esgoto",  Descricao = "agua e esgoto" },
                new Categoria { Titulo = "Transporte",  Descricao= "transporte" },
                new Categoria { Titulo = "Buraco",  Descricao= "buraco" },
                new Categoria { Titulo = "Iluminação",  Descricao= "iluminação" }
             };


            cat.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();

            var rec = new List<Reclamacao> {
                new Reclamacao {
                    Titulo = "Teste1",
                    Descricao = "teste1teste1",
                    DataRequisicao = DateTime.Parse("1989-1-11"),
                    CategoriaID =  cat.Single( e => e.Titulo == "Transporte").CategoriaID,
                  // Comentario = coment.FindAll(e => e.Descricao.Equals("legal")),
                    Status = "Em aberto!"
                },
                new Reclamacao {
                    Titulo = "Teste2",
                    Descricao = "teste2",
                    DataRequisicao = DateTime.Parse("1989-5-12"),
                    CategoriaID =  cat.Single( e => e.Titulo == "Transporte").CategoriaID,
                  // Comentario = coment.FindAll(e => e.Descricao.Equals("legal")),
                    Status = "Em aberto!"
                },
                new Reclamacao {
                    Titulo = "Teste1",
                    Descricao = "teste3",
                    DataRequisicao = DateTime.Parse("1989-5-12"),
                    CategoriaID =  cat.Single( e => e.Titulo == "Transporte").CategoriaID,
                  // Comentario = coment.FindAll(e => e.Descricao.Equals("legal")),
                    Status = "Em aberto!"
                }
            };
            rec.ForEach(s => context.Reclamacoes.Add(s));
            context.SaveChanges();

            var coment = new List<Comentario>
            {
                new Comentario {Descricao="legal esse problema", Data=DateTime.Parse("1987-2-11"), ReclamacaoID=2},
                new Comentario {Descricao="legal", Data=DateTime.Parse("1987-2-11"), ReclamacaoID=1},

            };
            coment.ForEach(s => context.Comentarios.Add(s));
            context.SaveChanges();
        }
    }
}