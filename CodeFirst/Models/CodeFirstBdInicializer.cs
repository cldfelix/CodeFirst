using System;
using System.Collections.Generic;
using System.Data.Entity;
using CodeFirst.Models.ClassesExemplo;

namespace CodeFirst.Models
{
    public class CodeFirstBdInicializer: DropCreateDatabaseAlways<DbContextCodeFirst>
    {
        protected override void Seed(DbContextCodeFirst ctx)
        {
            var cat1 = new Categoria
            {
                NomeCategoria = "Frutas",
                CriadaEm = DateTime.Now,
                EstaAtivo = true
            };

            var cat2 = new Categoria
            {
                NomeCategoria = "Limpeza",
                CriadaEm = DateTime.Now,
                EstaAtivo = true
            };

            var cat3 = new Categoria
            {
                NomeCategoria = "Variados",
                CriadaEm = DateTime.Now,
                EstaAtivo = true
            };


            var listaDeProdutos = new List<Produto>
            {
                new Produto{NomeProduto = "Alface", Preco = 2.50d, EstaAtivo = true, QtdEstoque = 5, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat1, cat2}},
                new Produto{NomeProduto = "Calça", Preco = 13.50d, EstaAtivo = true, QtdEstoque = 7, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat1}},
                new Produto{NomeProduto = "Bicicleta", Preco = 2.50d, EstaAtivo = true, QtdEstoque = 0, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat2}},
                new Produto{NomeProduto = "Arroz", Preco = 2.50d, EstaAtivo = true, QtdEstoque = 12, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat1, cat2, cat3}},
                new Produto{NomeProduto = "Carro", Preco = 65.50d, EstaAtivo = false, QtdEstoque = 21, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat1, cat2}},
                new Produto{NomeProduto = "Planta", Preco = 2.50d, EstaAtivo = true, QtdEstoque = 95, CriadaEm = DateTime.Now, Categorias = new List<Categoria>{cat1, cat2}}
            };


            listaDeProdutos.ForEach(p => ctx.Produtos.Add(p));

            ctx.SaveChanges();

        }
    }
}