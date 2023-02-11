using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels.Handle
{
	public class EntityClassAttribute : Attribute
	{
		public bool FullTextSearch { get; set; }
	}
}
