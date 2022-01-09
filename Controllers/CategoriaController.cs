using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using ControleEstoque2.Models;
using System.Globalization;
using System.Threading.Tasks;

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
        
    }
}