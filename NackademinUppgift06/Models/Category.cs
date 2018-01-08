using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NackademinUppgift06.Models
{
	public class Category
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		
		public virtual ICollection<Post> Posts { get; set; }
	}
}