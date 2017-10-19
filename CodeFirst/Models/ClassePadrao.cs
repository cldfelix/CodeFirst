using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CodeFirst.Models
{
    public class ClassePadrao
    {

        [Column("estaAtivo")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "estaAtivo")]
        public virtual bool EstaAtivo { get; set; }
        [Column("dataCriacao")] /* Define nome da coluna no db*/
        [JsonProperty(PropertyName = "criadoEm")]
        public virtual DateTime CriadaEm { get; set; }
    }
}