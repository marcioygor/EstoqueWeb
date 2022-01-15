using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using ControleEstoque2.Models;
using System.Globalization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ControleEstoque2.Controllers{
    
    public class CategoriaController : Controller{
        private readonly EstoqueWebContext _context;

        public CategoriaController(EstoqueWebContext context){
             this._context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Categorias.OrderBy(x=>x.Nome).AsNoTracking().ToListAsync());
        }

        [HttpGet]

        public async Task<ActionResult> Cadastrar(int? id)
        {
         if (id.HasValue)
         {
             var categoria=await _context.Categorias.FindAsync(id);
             if(categoria==null){
                 return NotFound();
             }

             return View(categoria);
         }

         return View(new CategoriaModel());
        }

        private bool CategoriaExiste(int id){
            return _context.Categorias.Any(x=>x.IdCategoria==id);
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(int? id, [FromForm] CategoriaModel categoria)
       {

         if(ModelState.IsValid){

         if (id.HasValue)
         {
           if(CategoriaExiste(id.Value)){
              _context.Categorias.Update(categoria);
              if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Categoria alterada com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao aletar categoria.", TipoMensagem.Erro);
              }
           }

           else{
               TempData["mensagem"]=MensagemModel.Serializar("Categoria não econtrada.", TipoMensagem.Erro);
           }
         }

         else{
             _context.Categorias.Add(categoria);
             if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Categoria cadastrada com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao cadastrar categoria.", TipoMensagem.Erro);
              }             
         }

                 
         return RedirectToAction(nameof(Index));

        }

          else{
           return View(categoria);
       }

       }

        [HttpGet]
        public async Task<ActionResult> Excluir(int? id)
        {
         if (!id.HasValue)
         {
             TempData["mensagem"]=MensagemModel.Serializar("Categoria não informada", TipoMensagem.Erro);
             return RedirectToAction(nameof(Index));
         }

        var categoria=await _context.Categorias.FindAsync(id);
        if(categoria==null){
            TempData["mensagem"]=MensagemModel.Serializar("Categoria não encontrada", TipoMensagem.Erro);
             return RedirectToAction(nameof(Index));
        }

         return View(categoria);
         
        }    

        [HttpPost]
        public async Task<ActionResult> Excluir(int id)
        {

         var categoria=await _context.Categorias.FindAsync(id);

         if(categoria!=null){
            _context.Categorias.Remove(categoria);

            if (await _context.SaveChangesAsync()>0)
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Categoria excluida com sucesso");
              }
              else
              {
                  TempData["mensagem"]=MensagemModel.Serializar("Erro ao excluir categoria.", TipoMensagem.Erro);
              }

              return RedirectToAction(nameof(Index));
         }

         else{
             TempData["mensagem"]=MensagemModel.Serializar("Categoria não encontrada");
             return RedirectToAction(nameof(Index));
         }   
        }
        
    }
}