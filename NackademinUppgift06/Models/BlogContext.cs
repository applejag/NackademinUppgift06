using Microsoft.EntityFrameworkCore;

namespace NackademinUppgift06.Models
{
	public class BlogContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }

		public BlogContext(DbContextOptions options)
			: base(options)
		{ }
	}
}