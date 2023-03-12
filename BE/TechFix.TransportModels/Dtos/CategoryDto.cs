using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class CategoryDto : IMapFrom<Category>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Path { get; set; }
        public string ConvertedName
        {
            get
            {
                int count = Path != null ? Path.Split('/').Length - 1 : 0;
                string result = "";
                if (count > 1)
                {
                    for (int i = 1; i < count; i++)
                    {
                        result += "|---";
                    }
                }
                return result + Name;
            }
        }
    }
}
