using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoFinal.Models
{
    public enum Status { Aberta, Encerrada, Resolvida }
    public class Reclamacao
    {
        public int ReclamacaoID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Status Status { get; set; }
        public string Usuario { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataRequisicao { get; set; }

        public string rua { get; set; }
        public string cep { get; set; }
        public string bairro { set; get; }

        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        [Display(Name = "Upload image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}