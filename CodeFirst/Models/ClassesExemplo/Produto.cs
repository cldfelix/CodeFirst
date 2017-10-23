using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CodeFirst.Models.ClassesExemplo
{
    [Table("tb_produtos", Schema = "codefirst")]
    public class Produto: ClassePadrao
    {
        [Key]
        [Column("idProduto")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "idProduto")]
        public virtual long IdProduto { get; set; }
        [Required(ErrorMessage = "O nome da categoria deve ser indicado!"), MaxLength(50, ErrorMessage = "O campo nome da categoria tem no máx. 50 letras")]
        [JsonProperty(PropertyName = "nomeDoProduto")]
        [Column("nomeDoProduto")] /* Define nome da coluna no db*/
        public virtual string NomeProduto { get; set; }
        [Column("preco")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "preco")]
        public virtual  double Preco { get; set; }
        [Column("qtdEstoque")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "qtdEmEstoque")]
        public virtual int QtdEstoque { get; set; }
        [Column("imagem")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "imagem")]
        public string Imagem { get; set; }
        [JsonProperty(PropertyName = "categorias")]
        public ICollection<Categoria> Categorias { get; set; }
    }
}