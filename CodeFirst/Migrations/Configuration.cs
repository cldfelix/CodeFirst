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

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirst.Models.DbContextCodeFirst>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirst.Models.DbContextCodeFirst context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DbContextCodeFirst()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DbContextCodeFirst()));


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
