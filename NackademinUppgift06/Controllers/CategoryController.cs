using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NackademinUppgift06.Models;

namespace NackademinUppgift06.Controllers
{
    public class CategoryController : Controller
    {
	    private readonly BlogContext context;

	    public CategoryController(BlogContext context)
	    {
		    this.context = context;
	    }

        public async Task<IActionResult> Create()
        {
		    ViewBag.Categories = await context.Categories.ToListAsync();

            return View();
		}

		[HttpPost]
	    public async Task<IActionResult> Create(Category category)
		{
			// Valid category?
			if (ModelState.IsValid)
			{
				await context.Categories.AddAsync(category);
				await context.SaveChangesAsync();

				ViewBag.wasCreated = true;
			}

		    ViewBag.Categories = await context.Categories.ToListAsync();
			return View(category);
		}
    }
}