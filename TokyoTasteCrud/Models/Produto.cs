using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Models;

namespace App.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string Descricao { get; set; }
        public float Preco { get; set; }
        public string Foto { get; set; }
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}