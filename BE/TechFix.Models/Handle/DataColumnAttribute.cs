using System;

namespace TechFix.EntityModels.Handle
{
	public class DataColumnAttribute : Attribute
	{
		public bool IgnoreSearch { get; set; }
	}
}
