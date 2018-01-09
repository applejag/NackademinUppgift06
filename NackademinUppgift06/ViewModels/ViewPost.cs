using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NackademinUppgift06.Models;

namespace NackademinUppgift06.ViewModels
{
    public class ViewPost
    {
		public Post CurrentPost { get; set; }
		public List<SelectListItem> Categories { get; }

	    public ViewPost()
			: this(null, null)
	    {}

	    public ViewPost(Post currentPost, IEnumerable<Category> categories)
	    {
		    CurrentPost = currentPost ?? new Post();

		    Categories = categories
				?.Select(c => new SelectListItem {Text = c.Name, Value = c.ID.ToString()}).ToList()
				?? new List<SelectListItem>();
	    }
    }
}
