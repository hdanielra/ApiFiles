using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
	public class Files
	{
		[Key]
		public int id { get; set; }
		public string name { get; set; }
		public string ext { get; set; }
		public double length { get; set; }
		public string path { get; set; }

	}
}
