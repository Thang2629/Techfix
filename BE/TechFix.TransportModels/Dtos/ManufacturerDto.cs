﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class ManufacturerDto : IMapFrom<Manufacturer>
    {
        public string Name { get; set; }
    }
}
