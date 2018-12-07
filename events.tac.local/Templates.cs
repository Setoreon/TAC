using Sitecore.Data;

namespace events.tac.local
{
    public struct Templates
    {
        public struct EventDetails
        {
            public static readonly ID ID = new ID("{630203E6-A53C-473C-9E45-C1880F37EF87}");

            public struct Fields
            {
                public static readonly ID Highlights = new ID("{3B6202C4-FE19-4474-A044-730D7844EF1D}");
                public static readonly ID EventImage = new ID("{E3DDA2A1-43E5-46C5-8432-37C181A3512E}");
                public static readonly ID StartDate = new ID("{8A00E1B6-5631-40DE-BC8D-DBAD7B345473}");
                public static readonly ID Duration = new ID("{DBDBCA0E-8917-4E40-9618-63944C765134}");
                public static readonly ID DifficultyLevel = new ID("{AE57F69E-C177-4FBE-BA44-A1EFE5939235}");
                public static readonly ID LikeCount = new ID("{D482F84E-0BA7-4238-9F9F-6297D599EFD8}");
                public static readonly ID RelatedEvents = new ID("{6CDFB1E0-041D-4F93-B0F0-C3BC633220C1}");
            }
        }
    }
}