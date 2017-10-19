using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst.Models.Infrastructure
{
    public class SegurancaInicializacao : DropCreateDatabaseAlways<SegurancaDbContext>
    {
        protected override void Seed(SegurancaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SegurancaDbContext()));

            var user = new ApplicationUser
            {
                UserName = "SuperPowerUser",
                Email = "taiseer.joudeh@mymail.com",
                EmailConfirmed = true,
                Nome = "Taiseer",
                Sobrenome = "Joudeh",
                Nivel = 1,
                DataDeCriacao = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "MySuperP@ssword!");
        }
    }
}


