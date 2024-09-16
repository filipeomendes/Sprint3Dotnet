using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint3Dotnet.Models
{
    [Table("T_LOJA")]
    public class Loja
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NOME", TypeName = "VARCHAR2(100)")]
        public string Nome { get; set; }

        [Required]
        [Column("CNPJ", TypeName = "VARCHAR2(14)")]
        public string CNPJ { get; set; }
    }
}
