﻿using System.Threading.Tasks;
using HogwartsPotions.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Controllers
{
    public class RecipesController : Controller
    {
        private readonly HogwartsContext _context;

        public RecipesController(HogwartsContext context)
        {
            _context = context;
        }
        [Route("Recipes")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.Include(p => p.Ingredients).Include(m => m.Student).ToListAsync());
        }
    }
}