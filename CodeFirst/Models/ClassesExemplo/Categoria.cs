using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CodeFirst.Models.ClassesExemplo
{
    [Table("tb_categorias", Schema = "codefirst")]
    public class Categoria: ClassePadrao
    {
        [Key]
        [Column("idCategoria")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "idCategoria")]
        public virtual long IdCategoria { get; set; }
        
        [Required(ErrorMessage = "O nome da categoria deve ser indicado!"), MaxLength(50, ErrorMessage = "O campo nome da categoria tem no máx. 50 letras")]
        [JsonProperty(PropertyName = "nomeCategoria")]
        public virtual string NomeCategoria { get; set; }
        
        [JsonProperty(PropertyName = "produtos")]
        public ICollection<Produto> Produtos { get; set; }
    }
}