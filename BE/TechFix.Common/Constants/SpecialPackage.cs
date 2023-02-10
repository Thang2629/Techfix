using System;
using System.Collections.Generic;

namespace TechFix.Common.Constants
{
    public class SpecialPackage
    {
        public static readonly Guid VlinkGratefulId = Guid.Parse("ba7397ad-fd2b-11eb-acc1-4e6b9e3ff844");
        public static readonly List<Guid?> VmmIds = new()
        {
            Guid.Parse("df08070c-9622-11ec-9a87-4e6b9e3ff844"),
            Guid.Parse("cb928ea8-8c02-460b-9371-f84b82db698f"),
            Guid.Parse("2a80f0ef-35a6-42b5-bbe9-83920e31615b"),
            Guid.Parse("7d32457b-43a7-40c2-a848-b130d641dc12"),
            Guid.Parse("99255edb-a986-4508-a8a8-4c82d82b6e3d"),

        };
        public static readonly List<Guid?> L1Ids = new ()
        {
            Guid.Parse("d6169dd9-0c8d-11ed-a7a3-4e6b9e3ff844"),
            Guid.Parse("ff8f1add-d09f-452f-a455-8de9b9bdf6b2"),
            Guid.Parse("ceaa0491-327b-418f-85b1-a7b47babd565"),
            Guid.Parse("8333b4d2-ac00-4d8b-ac2f-514946828772"),
            Guid.Parse("758f13bf-4ca4-418e-b647-f279f820952c"),
        };
        public static readonly List<Guid?> EB3Ids = new()
        {
            Guid.Parse("d6283a94-0c8d-11ed-a7a3-4e6b9e3ff844"),
            Guid.Parse("56cd658e-ac0c-42fd-8684-7db5d2e069f1"),
            Guid.Parse("077a4ada-bfe7-4683-9579-72d7cbc98e99"),
            Guid.Parse("9884f7b4-0c8b-4016-aa1b-4bc2e69cd078"),
            Guid.Parse("9245a4c0-898a-49f9-a542-3baa06bf2701"),
        };
        public static readonly List<Guid?> EB5Ids = new()
        {
            Guid.Parse("d6392b38-0c8d-11ed-a7a3-4e6b9e3ff844"),
            Guid.Parse("0d63cad9-dc82-422d-9bd9-dcf710ea5aff"),
            Guid.Parse("5c58a91f-01ee-4a4c-976b-05a274fae4bf"),
            Guid.Parse("d3ce33d0-8098-458f-a9ee-4e4f293596a6"),
        };
        public static readonly Guid RookieBusinessFreeId = Guid.Parse("e1e7cf61-e275-11eb-acc1-4e6b9e3ff844");
        public static readonly Guid Vlg10K = Guid.Parse("54599dcd-afd3-439a-9331-d56d335b0d4f");
        public static readonly Guid Vlg50K = Guid.Parse("6058765c-a465-4eda-a5c9-01a3f66f9735");
        public static readonly Guid ProBusiness = Guid.Parse("3dcc1145-e275-11eb-acc1-4e6b9e3ff844");
        public static readonly Guid PlatinumBusiness = Guid.Parse("9552f268-e275-11eb-acc1-4e6b9e3ff844");
        public static readonly Guid BronzeBusiness = Guid.Parse("b6c44663-e275-11eb-acc1-4e6b9e3ff844");

        public static readonly Guid Vmm3K = Guid.Parse("2a80f0ef-35a6-42b5-bbe9-83920e31615b");
        public static readonly Guid Stater = Guid.Parse("b06b644b-e1c9-11ec-9f32-4e6b9e3ff844");

        public static readonly Guid Platinum = Guid.Parse("58ab1b7a-2435-4b6e-a49f-4a915fff8feb");
        public static readonly Guid Bronze = Guid.Parse("c7167083-d3bd-4d8f-99e0-a75dd199dc93");
        public static readonly List<Guid?> PlatformPackageIds = new() { ProBusiness, PlatinumBusiness, BronzeBusiness };

    }
}

