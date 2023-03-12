using System;

namespace TechFix.EntityModels.Handle
{
	public class EntityClassAttribute : Attribute
	{
		public bool FullTextSearch { get; set; }
	}
}
