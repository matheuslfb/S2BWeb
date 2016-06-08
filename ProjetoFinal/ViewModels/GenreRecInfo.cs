using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoFinal.ViewModels
{
    public class GenreRecInfo
    {

        [Display(Name = "Name")]
        [Key]
        public string name { get; set; }

        [Display(Name = "Count")]
        public int count { get; set; }


    }
}