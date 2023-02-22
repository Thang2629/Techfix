using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class TemplateTransport
    {
        public string Code { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
