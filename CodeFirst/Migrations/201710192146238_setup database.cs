namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setupdatabase : DbMigration
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
                        estaAtivo = c.Boolean(nullable: false),
                        dataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idProduto);
            
            CreateTable(
                "dbo.ProdutoCategorias",
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
            DropForeignKey("dbo.ProdutoCategorias", "Categoria_IdCategoria", "codefirst.tb_categorias");
            DropForeignKey("dbo.ProdutoCategorias", "Produto_IdProduto", "codefirst.tb_produtos");
            DropIndex("dbo.ProdutoCategorias", new[] { "Categoria_IdCategoria" });
            DropIndex("dbo.ProdutoCategorias", new[] { "Produto_IdProduto" });
            DropTable("dbo.ProdutoCategorias");
            DropTable("codefirst.tb_produtos");
            DropTable("codefirst.tb_categorias");
        }
    }
}
