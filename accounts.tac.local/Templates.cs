using Sitecore.Data;

namespace accounts.tac.local
{
    public struct Templates
    {
        public static readonly ID UserProfileDefault = new ID("{9589DA41-665E-4A95-9A40-3F51FF1AD2D8}");
        
        public struct AccountsSettings
        {
            public static readonly ID ID = new ID("{98FA9163-863C-427A-9E69-F9EF24C6E04D}");

            public struct Fields
            {
                public static readonly ID AccountsDetailsPage = new ID("{615E55A4-2632-411B-8DEF-81674DB03893}");
                public static readonly ID RegisterPage = new ID("{A3084A46-CE39-48FD-AD65-FF188EEA3336}");
                public static readonly ID LoginPage = new ID("{E8D84858-63EC-4B1B-9152-DE6084AE2689}");
                public static readonly ID ForgotPasswordPage = new ID("{CC74DE85-A2F5-4632-B372-B56515ECF28B}");
                public static readonly ID AfterLoginPage = new ID("{67E9859E-3B77-4A55-8368-F04469D7D2AA}");
                public static readonly ID ForgotPasswordMailTemplate = new ID("{03F72D33-D310-4E33-A977-E1ADCFE979A8}");
                public static readonly ID RegisterOutcome = new ID("{F88C7634-7A9B-417D-AE97-8B1573F60187}");
            }
        }
        
        public struct ProfileSettigs
        {
            public static readonly ID ID = new ID("{D5C26DD6-469D-4F0D-8136-60EF902821D0}");

            public struct Fields
            {
                public static readonly ID UserProfile = new ID("{1AD3E7B9-32F6-41A7-A737-E831DEBC5E9A}");
                public static readonly ID InterestsFolder = new ID("{BBD14D86-0BC4-4BA1-9E75-9D9F73AD8005}");
            }
        }
        
        public struct Interest
        {
            public static readonly ID ID = new ID("{FD4F5FD3-B881-42BD-84E6-6B2A35203085}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{D1EA482A-3B35-49AB-983F-D39F621817FA}");
            }
        }

        public struct MailTemplate
        {
            public static readonly ID ID = new ID("{141DDF5B-7774-4327-A41D-CB7F21768BDD}");

            public struct Fields
            {
                public static readonly ID From = new ID("{A84351BE-9DCD-4FFE-B81D-A968F9F8B627}");
                public static readonly ID Subject = new ID("{594DAEAA-85EB-4197-80C9-7054FAF39E5D}");
                public static readonly ID Body = new ID("{EE554425-BC8E-42B0-9A37-F04FF3FE3D7C}");
            }
        }

        public struct UserProfile
        {    
            public static readonly ID ID = new ID("{451C599D-44E7-4E06-ABAE-E0037A248AFB}");

            public struct Fields
            {
                public static readonly ID FirstName = new ID("{9BBF79E7-9575-494F-B154-6BB305542C90}");
                public static readonly ID LastName = new ID("{D0CC4C9B-ADD7-43BB-9612-F968EB77D0A2}");
                public static readonly ID PhoneNumber = new ID("{B8E73011-3FB1-4312-8BFF-7F5E2D9F78C0}");
            }
        }

        public struct LoginTeaser
        {
            public static readonly ID ID = new ID("{D3E55A91-5D87-4E19-BD9E-AD0B17269529}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{8801542A-B5DD-4A13-B5A2-75383A36405D}");
                public static readonly ID Summary = new ID("{5BFF1198-1619-4861-94F6-17FA082AE753}");
                public static readonly ID LoggedInTitle = new ID("{EB1F7D1C-A839-4AD7-B765-A5803521120F}");
                public static readonly ID LoggedInSummary = new ID("{1DEE9DD6-AA71-4805-8003-2E0B96E0C11B}");
            }
        }
    }
}