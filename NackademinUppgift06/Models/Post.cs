using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NackademinUppgift06.Models
{
	public class Post
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[MaxLength(50)]
		public string Title { get; set; }

		[Required]
		[MaxLength(2000)]
		public string Content { get; set; }
		
		public DateTime CreatedAt { get; set; }

		[ForeignKey(nameof(Category))]
		public int CategoryID { get; set; }
		public virtual Category Category { get; set; }

		public Post()
		{
			CreatedAt = DateTime.Now;
		}
	}
}