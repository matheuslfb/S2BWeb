namespace ProjetoFinal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<ProjetoFinal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ProjetoFinal.Models.ApplicationDbContext";
        }

        protected override void Seed(ProjetoFinal.Models.ApplicationDbContext context)
        {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Oficial"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Oficial" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Padrao"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Padrao" };

                manager.Create(role);
            }

            var hasher = new PasswordHasher();
            var users = new List<ApplicationUser> {

            new ApplicationUser() {   UserName = "admin@s2b.pucrs.br", PasswordHash = hasher.HashPassword("s2b") },

            new ApplicationUser() { UserName = "smov@s2b.pucrs.br", PasswordHash = hasher.HashPassword("s2b")},

            new ApplicationUser { UserName = "sman@s2b.pucrs.br",  PasswordHash = hasher.HashPassword("s2b") },

            new ApplicationUser{ UserName = "hugo@s2b.pucrs.br", PasswordHash = hasher.HashPassword("s2b")},

            new ApplicationUser{  UserName = "ze@s2b.pucrs.br",  PasswordHash = hasher.HashPassword("s2b")},

            new ApplicationUser{ UserName = "luis@s2b.pucrs.br", PasswordHash = hasher.HashPassword("s2b")},

        };
            foreach (ApplicationUser u in users)
            {
                UserManager.Create(u);
                context.Users.AddOrUpdate(u);
            
                if (u.UserName.Equals("admin@s2b.pucrs.br"))
                {
                    UserManager.AddToRole(u.Id, "Admin");
                }
                else if (u.UserName.Equals("smov@s2b.pucrs.br") || u.UserName.Equals("sman@s2b.pucrs.br"))
                {
                    UserManager.AddToRole(u.Id, "Oficial");
                }
                else
                    UserManager.AddToRole(u.Id, "Padrao");
            }
        }
    }
}
