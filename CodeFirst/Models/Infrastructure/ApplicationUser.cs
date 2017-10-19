using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst.Models.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sobrenome { get; set; }

        [Required]
        public byte Nivel { get; set; }

        [Required]
        public DateTime DataDeCriacao { get; set; }
    }
}