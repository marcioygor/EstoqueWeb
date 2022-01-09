using System;
using System.ComponentModel.DataAnnotations;

namespace ControleEstoque2.Models
{

    public class CategoriaModel
    {
        [Key]
        public int IdCategoria { get; set; }
        
        [Required, MaxLength(120)]
        public string Nome { get; set; }

    }


}