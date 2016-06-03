using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoFinal.Models
{
    public class Reclamacao
    {
        public int ReclamacaoID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataRequisicao { get; set; }
      
        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Upload image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}