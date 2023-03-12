using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class TemplateDto : IMapFrom<Template>
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        [DisplayName("FileName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

    }
}
