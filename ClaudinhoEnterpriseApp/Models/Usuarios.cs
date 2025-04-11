using System.ComponentModel.DataAnnotations.Schema;

namespace ClaudinhoEnterpriseApp.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;

        [Column("DataDeCriacao")]  // Match exact database column name
        public DateTime DataDeCriacao { get; set; } = DateTime.UtcNow;

        [Column("TipoDeUsuario")]  // Match exact database column name
        public string TipoDeUsuario { get; set; } = "Cliente";
    }
}