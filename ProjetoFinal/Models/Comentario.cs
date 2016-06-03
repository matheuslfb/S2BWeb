using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Comentario
    {
        public int ComentarioID { get; set; }
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public int ReclamacaoID { get; set; }
        public virtual Reclamacao Reclamacao { get; set; }


        [Display(Name = "Upload image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }
        
    }
}