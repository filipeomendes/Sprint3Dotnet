using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint3Dotnet.Models
{
    [Table("T_CLIENTE")]
    public class Cliente
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NOME_CLIENTE", TypeName = "VARCHAR2(80)")]
        public string Nome { get; set; }

        [Required]
        [Column("DATA_NASCIMENTO", TypeName = "DATE")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Column("CPF", TypeName = "VARCHAR2(11)")]
        public string CPF { get; set; }

        [Required]
        [Column("TELEFONE", TypeName = "VARCHAR2(11)")]
        public string Telefone { get; set; }

        [Required]
        [Column("ENDERECO", TypeName = "VARCHAR2(200)")]
        public string Endereco { get; set; }

        [Required]
        [Column("EMAIL", TypeName = "VARCHAR2(100)")]
        public string Email { get; set; }
    }
}
