using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
	public class VlinkSequence
	{
		public int Id { get; set; }
		public string SequenceName { get; set; }
		public int Value { get; set; }
	}
}
