using CodeFirst.Models;
using CodeFirst.Models.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirst.Models.SegurancaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirst.Models.SegurancaDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SegurancaDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SegurancaDbContext()));


            var user = new ApplicationUser
            {
                UserName = "SuperUsuario",
                Email = "claudinei.felix@outlook.com",
                EmailConfirmed = true,
                Nome = "Claudinei",
                Sobrenome = "Joudeh",
                Nivel = 1,
                DataDeCriacao = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "MySuperP@ssword!");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Usuario" });
            }

            var adminUser = manager.FindByName("SuperPowerUser");

            manager.AddToRoles(adminUser.Id, "SuperAdmin", "Admin");
        }
        
    }
}
