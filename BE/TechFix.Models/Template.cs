using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
    public class Template : BaseModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }
    }
}
