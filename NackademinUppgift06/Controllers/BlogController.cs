using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NackademinUppgift06.Models;
using NackademinUppgift06.ViewModels;

namespace NackademinUppgift06.Controllers
{
    public class BlogController : Controller
    {
	    private readonly BlogContext context;

	    public BlogController(BlogContext context)
	    {
		    this.context = context;
	    }

	    public async Task<IActionResult> Create()
	    {
		    // No categories?
		    if (!await context.Categories.AnyAsync())
			    return RedirectToAction(actionName: "Create", controllerName: "Category");

			List<Category> categories = await context.Categories.ToListAsync();

			return View(new ViewPost(null, categories));
		}

		[HttpPost]
	    public async Task<IActionResult> Create(ViewPost post)
		{
			// No categories?
			if (!await context.Categories.AnyAsync())
				return RedirectToAction(actionName: "Create", controllerName: "Category");

			// Valid post?
		    if (ModelState.IsValid)
		    {
			    await context.Posts.AddAsync(post.CurrentPost);
			    //context.Attach(post.CurrentPost.Category);
			    await context.SaveChangesAsync();

			    return RedirectToAction("Post", post.CurrentPost.ID);
		    }

			List<Category> categories = await context.Categories.ToListAsync();

			return View(new ViewPost(post.CurrentPost, categories: categories));
	    }

	    public IActionResult Post(int id)
	    {
		    Post post = context.Posts
				.Include(p => p.Category)
				.SingleOrDefault(p => p.ID == id);

		    if (post == default(Post))
			    return RedirectToAction("Index");

		    return View(post);
		}

	    public async Task<IActionResult> Index()
	    {
			ViewBag.PostsCount = await context.Posts.CountAsync();

		    ViewBag.Posts = await context.Posts
			    .Include(p => p.Category)
			    .OrderByDescending(p => p.CreatedAt)
				.ThenBy(p => p.Title)
			    .ToListAsync();

			return View();
		}

		public async Task<IActionResult> Search(ViewSearch search)
		{
			ViewBag.PostsCount = await context.Posts.CountAsync();

		    ViewBag.Posts = await context.Posts
				.Include(p => p.Category)
				.OrderByDescending(p => p.CreatedAt)
				.ThenBy(p => p.Title)
				.Where(p => SearchPost(search.Query, p))
				.ToListAsync();
			
		    return View("Index", search);
	    }

	    private static bool SearchPost(string needle, Post haystack)
	    {
		    return SearchString(needle, haystack.Category.Name)
				|| SearchString(needle, haystack.Title)
				|| SearchString(needle, haystack.Content);
	    }

	    private static bool SearchString(string needle, string haystack)
	    {
		    return haystack?.IndexOf(needle?.Trim() ?? string.Empty, StringComparison.CurrentCultureIgnoreCase) != -1;
	    }
    }
}