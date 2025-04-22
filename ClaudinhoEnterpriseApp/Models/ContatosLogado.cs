using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaudinhoEnterpriseApp.Models
{
    [Table("contatoslogado")]
    public class ContatoLogado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idusuario")]  // mapeia a coluna do banco
        public int IdUsuario { get; set; }

        [Column("assunto")]
        public string Assunto { get; set; } = string.Empty;

        [Column("mensagem")]
        public string Mensagem { get; set; } = string.Empty;

        [Column("datacriacao")]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;


        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
