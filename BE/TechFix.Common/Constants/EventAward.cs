using System;
using System.Collections.Generic;

namespace TechFix.Common.Constants
{
    public class EventAward
    {
        public static readonly Guid Iphone = Guid.Parse("a7ca0657-fb1f-41d2-8e89-82bee03c5bc8");
        public static readonly Guid Macbook = Guid.Parse("7800e407-e447-4013-b34c-ccc2a4d90afe");
        public static readonly Guid Ipad = Guid.Parse("07bb4a8b-87ff-4aa1-b20a-bcbcb1d7c56d");
        public static readonly Guid AmazonGiftCard = Guid.Parse("6e01543e-3736-436f-b8f8-5a691a11379c");
        public static readonly Guid VlinknailGiftCard = Guid.Parse("f14107cb-5f42-4910-8d0a-a15f5822ac24");
        public static readonly Guid VlinkpayGiftCard = Guid.Parse("719837e9-7468-4797-b936-7efb93497f22");
        public static readonly Guid VmmToken = Guid.Parse("79d07001-8c6a-42dc-ad83-2d6721188175");
        public static readonly Guid VmmPackage = Guid.Parse("e5fab8bd-3f5d-48e7-bcc7-d57b479679da");
        public static readonly List<Guid> AllEventAwardIds = new List<Guid> { Iphone, Macbook, Ipad, AmazonGiftCard, VlinknailGiftCard, VlinkpayGiftCard, VmmToken, VmmPackage };
    }
}

