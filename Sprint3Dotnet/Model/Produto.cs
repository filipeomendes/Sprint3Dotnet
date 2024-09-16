using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint3Dotnet.Models
{
    [Table("T_PRODUTO")]
    public class Produto
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NOME_PRODUTO", TypeName = "VARCHAR2(100)")]
        public string NomeProduto { get; set; }

        [Required]
        [Column("CATEGORIA", TypeName = "VARCHAR2(50)")]
        public string Categoria { get; set; }

        [Required]
        [Column("PRECO", TypeName = "NUMBER(6,2)")]
        public decimal Preco { get; set; }
    }
}
