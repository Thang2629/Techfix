using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.Common.Constants
{
    public class ImageStatus
    {
        public const string Active = "Active";
        public const string Inactive = "Inactive";
    }
    public class ImageType
    {
        public const string Banner = "Banner";
        public const string NewsAndEvent = "NewsAndEvent";// TODO: Remove conflict migration
        public const string News = "News";
        public const string Event = "Event";
    }
}
