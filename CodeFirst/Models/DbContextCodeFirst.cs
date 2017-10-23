using System.Data.Entity;
using CodeFirst.Models.ClassesExemplo;
using CodeFirst.Models.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst.Models
{
    public class DbContextCodeFirst : DbContext
    {
        public DbContextCodeFirst()
            : base("ConexaoDbCodeFirst")
        {
            /* Inicializa o banco de dados com valores setados */
            Database.SetInitializer(new CodeFirstBdInicializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* Alterar o dbo padrao */
            modelBuilder.HasDefaultSchema("seguranca");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("tbl_usuario", "seguranca").HasKey<string>(l => l.UserId);
            modelBuilder.Entity<ApplicationUser>().ToTable("tbl_usuario", "seguranca"); //.HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().ToTable("tbl_roles", "seguranca").HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().ToTable("tbl_usuarioRoles", "seguranca").HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<IdentityUserClaim>().ToTable("tbl_usuarioClaims", "seguranca").HasKey<int>(r => r.Id);
            modelBuilder.Entity<IdentityUserLogin>().ToTable("tbl_usuarioLogins", "seguranca").HasKey<string>(r => r.UserId);
           
        }
        public static DbContextCodeFirst Create()
        {
            return new DbContextCodeFirst();
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
    }



    /*
    public class SegurancaDbContext : IdentityDbContext<ApplicationUser>
    {
        public SegurancaDbContext()
            : base("ConexaoDbCodeFirst", throwIfV1Schema: false)
        {

            // Database.SetInitializer(new SegurancaInicializacao());
            // Database.SetInitializer<SegurancaDbContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* Alterar o dbo padrao 
             modelBuilder.HasDefaultSchema("seguranca");
             modelBuilder.Entity<IdentityUserLogin>().ToTable("tbl_usuario", "seguranca").HasKey<string>(l => l.UserId);
             modelBuilder.Entity<ApplicationUser>().ToTable("tbl_usuario", "seguranca"); //.HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().ToTable("tbl_roles", "seguranca").HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().ToTable("tbl_usuarioRoles", "seguranca").HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<IdentityUserClaim>().ToTable("tbl_usuarioClaims", "seguranca").HasKey<int>(r => r.Id); 
            modelBuilder.Entity<IdentityUserLogin>().ToTable("tbl_usuarioLogins", "seguranca").HasKey<string>(r => r.UserId);
        }

        public static SegurancaDbContext Create()
        {
            return new SegurancaDbContext();
        }

    }
*/
}