using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using ControleEstoque2.Models;
using System.Globalization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleEstoque2.Controllers{
    
    public class ProdutoController : Controller{
        private readonly EstoqueWebContext _context;

        public ProdutoController(EstoqueWebContext context){
             this._context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Produtos.OrderBy(x=>x.Nome).Include(x=>x.Categoria).AsNoTracking().ToListAsync());
        }

        [HttpGet]

        public async Task<ActionResult> Cadastrar(int? id)
        {
         var categorias= _context.Categorias.OrderBy(x=>x.Nome).AsNoTracking().ToList();
         var categoriasSelectList=new SelectList(categorias, nameof(CategoriaModel.IdCategoria),nameof(CategoriaModel.Nome));
         ViewBag.Categorias=categoriasSelectList;

         if (id.HasValue)
         {
             var produto=await _context.Produtos.FindAsync(id);
             if(produto==null){
                 return NotFound();
             }

             return View(produto);
         }

         return View(new ProdutoModel());
        }

        private bool ProdutoExiste(int id){
            return _context.Produtos.Any(x=>x.IdProduto==id);
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(int? id, [FromForm] ProdutoModel produto)
       {

         if(ModelState.IsValid){

         if (id.HasValue)
         {
           if(ProdutoExiste(id.Value)){
              _context.Produtos.Update(produto);
              if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Produto alterado com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao aletar produto.", TipoMensagem.Erro);
              }
           }

           else{
               TempData["mensagem"]=MensagemModel.Serializar("Produto n??o econtrado.", TipoMensagem.Erro);
           }
         }

         else{
             _context.Produtos.Add(produto);
             if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Produto cadastrado com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao cadastrar produto.", TipoMensagem.Erro);
              }             
         }

                 
         return RedirectToAction(nameof(Index));

        }

          else{
           return View(produto);
       }

       }

        [HttpGet]
        public async Task<ActionResult> Excluir(int? id)
        {
         if (!id.HasValue)
         {
             TempData["mensagem"]=MensagemModel.Serializar("Produto n??o informado", TipoMensagem.Erro);
             return RedirectToAction(nameof(Index));
         }

        var produto=await _context.Produtos.FindAsync(id);
        if(produto==null){
            TempData["mensagem"]=MensagemModel.Serializar("Produto n??o encontrado", TipoMensagem.Erro);
             return RedirectToAction(nameof(Index));
        }

         return View(produto);
         
        }    

        [HttpPost]
        public async Task<ActionResult> Excluir(int id)
        {

         var produto=await _context.Produtos.FindAsync(id);

         if(produto!=null){
            _context.Produtos.Remove(produto);

            if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Produto excluido com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao excluir produto.", TipoMensagem.Erro);
              }

              return RedirectToAction(nameof(Index));
         }

         else{
             TempData["mensagem"]=MensagemModel.Serializar("Produto n??o encontrado");
             return RedirectToAction(nameof(Index));
         }   
        }
        
    }
}