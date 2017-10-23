﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodeFirst.Models;
using CodeFirst.Models.ClassesExemplo;

namespace CodeFirst.Controllers
{
    public class ProdutosController : ApiController
    {
        private DbContextCodeFirst db = new DbContextCodeFirst();

        // GET: api/Produtos
        public List<Produto> GetProdutos()
        {
            var p = db.Produtos.ToList();

            return p;
        }

        // GET: api/Produtos/5
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> GetProduto(long id)
        {
            Produto produto = await db.Produtos.Include("Categorias").FirstOrDefaultAsync(p => p.IdProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduto(long id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.IdProduto)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produtos
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produtos.Add(produto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = produto.IdProduto }, produto);
        }

        // DELETE: api/Produtos/5
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> DeleteProduto(long id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(long id)
        {
            return db.Produtos.Count(e => e.IdProduto == id) > 0;
        }
    }
}