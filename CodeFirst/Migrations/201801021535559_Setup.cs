namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Setup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "codefirst.tb_categorias",
                c => new
                    {
                        idCategoria = c.Long(nullable: false, identity: true),
                        NomeCategoria = c.String(nullable: false, maxLength: 50),
                        estaAtivo = c.Boolean(nullable: false),
                        dataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "codefirst.tb_produtos",
                c => new
                    {
                        idProduto = c.Long(nullable: false, identity: true),
                        nomeDoProduto = c.String(nullable: false, maxLength: 50),
                        preco = c.Double(nullable: false),
                        qtdEstoque = c.Int(nullable: false),
                        imagem = c.String(),
                        estaAtivo = c.Boolean(nullable: false),
                        dataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idProduto);
            
            CreateTable(
                "seguranca.tbl_usuarioLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("seguranca.tbl_usuario", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "seguranca.tbl_usuario",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Sobrenome = c.String(nullable: false, maxLength: 100),
                        Nivel = c.Short(nullable: false),
                        DataDeCriacao = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "seguranca.tbl_usuarioClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("seguranca.tbl_usuario", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "seguranca.tbl_usuarioRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("seguranca.tbl_usuario", t => t.ApplicationUser_Id)
                .ForeignKey("seguranca.tbl_roles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "seguranca.tbl_roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "seguranca.ProdutoCategorias",
                c => new
                    {
                        Produto_IdProduto = c.Long(nullable: false),
                        Categoria_IdCategoria = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Produto_IdProduto, t.Categoria_IdCategoria })
                .ForeignKey("codefirst.tb_produtos", t => t.Produto_IdProduto, cascadeDelete: true)
                .ForeignKey("codefirst.tb_categorias", t => t.Categoria_IdCategoria, cascadeDelete: true)
                .Index(t => t.Produto_IdProduto)
                .Index(t => t.Categoria_IdCategoria);
            
        }
        
        public override void Down()
        {
            DropForeignKey("seguranca.tbl_usuarioRoles", "IdentityRole_Id", "seguranca.tbl_roles");
            DropForeignKey("seguranca.tbl_usuarioRoles", "ApplicationUser_Id", "seguranca.tbl_usuario");
            DropForeignKey("seguranca.tbl_usuarioLogins", "ApplicationUser_Id", "seguranca.tbl_usuario");
            DropForeignKey("seguranca.tbl_usuarioClaims", "ApplicationUser_Id", "seguranca.tbl_usuario");
            DropForeignKey("seguranca.ProdutoCategorias", "Categoria_IdCategoria", "codefirst.tb_categorias");
            DropForeignKey("seguranca.ProdutoCategorias", "Produto_IdProduto", "codefirst.tb_produtos");
            DropIndex("seguranca.ProdutoCategorias", new[] { "Categoria_IdCategoria" });
            DropIndex("seguranca.ProdutoCategorias", new[] { "Produto_IdProduto" });
            DropIndex("seguranca.tbl_usuarioRoles", new[] { "IdentityRole_Id" });
            DropIndex("seguranca.tbl_usuarioRoles", new[] { "ApplicationUser_Id" });
            DropIndex("seguranca.tbl_usuarioClaims", new[] { "ApplicationUser_Id" });
            DropIndex("seguranca.tbl_usuarioLogins", new[] { "ApplicationUser_Id" });
            DropTable("seguranca.ProdutoCategorias");
            DropTable("seguranca.tbl_roles");
            DropTable("seguranca.tbl_usuarioRoles");
            DropTable("seguranca.tbl_usuarioClaims");
            DropTable("seguranca.tbl_usuario");
            DropTable("seguranca.tbl_usuarioLogins");
            DropTable("codefirst.tb_produtos");
            DropTable("codefirst.tb_categorias");
        }
    }
}
