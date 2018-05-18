using System;
using System.Collections.Generic;
using System.Linq;
namespace Bakery
{
    public static class GlobalSettings
    {
        /// <summary>
        /// Exigo-specific API credentials and configurations
        /// </summary>
        public static class Exigo
        {
            /// <summary>
            /// Web service, OData and SQL API credentials and configurations
            /// </summary>
            public static class Api
            {
                public static string LoginName = "API";
                public static string Password = "api123";
                public static string CompanyKey = "PHP";


                /// <summary>
                /// DEPLOYMENT SETTINGS
                /// ONLY WHEN USING SANDBOX WILL USESANDBOXGLOBALLY NEED TO BE CHANGED
                /// IsLive is only for hiding Azure Insights in UAT 
                /// </summary>
                public static bool UseSandboxGlobally = false;
                public static bool IsLive = false;

                public static int SandboxID { get { return (UseSandboxGlobally) ? 1 : 0; } }

                /// <summary>
                /// Replicated SQL connection strings and configurations
                /// 
                /// </summary>
                public static class Sql
                {
                    public static class ConnectionStrings

                    {
                        public static string SqlReporting

                        {
                            get
                            {

                                if (!Exigo.Api.UseSandboxGlobally)
                                {
                                    // Live 
                                    return
                                        "Data Source=336-PHPSQL1.vm.epicservers.com;Initial Catalog=phpreporting;Persist Security Info=True;User ID=phpweb;Password=j5UAY$V%&X5^NtFg;Pooling=True";
                                }
                                else
                                {
                                    // Sandboxes 1 - 4
                                    return String.Format("Data Source=sandbox.bi.exigo.com;Initial Catalog=phpReportingSandbox{0};Persist Security Info=True;User ID=PHPSQL;Password=Afyzzn%$S919;Pooling=False", SandboxID);
                                }
                            }
                        }

                    }
                }
            }

            public static class Caching
            {
                /// <summary>
                /// Connection string for redis caching
                /// </summary>
                public const string RedisConnectionString = "php.redis.cache.windows.net:6380,password=pUf9iaVfRbta2NEkORXsbK5WIxcjTYvsk4elYbSsX8c=,ssl=True,abortConnect=False";

                /// <summary>
                /// Cache time in minutes
                /// </summary>
                public static int CacheTimeout = 120;

                public static bool Enabled = true;
                public static int Lifespan = 300000;
                public static string Schema = "_cache";
            }

            /// <summary>
            /// Web Session Settings
            /// </summary>
            public static class UserSession
            {
                /// <summary>
                /// Set to True for SQL session caching
                /// Set to False for Redis session caching
                /// </summary>
                public static bool UseDbSessionCaching = false;

                /// <summary>
                /// Set to true to use SQL memory optimized tables.
                /// The Sql server must be set up to support this.
                /// </summary>
                public static bool UseOltpInMemory = false;

                //public static string StorageType = (UseDbSessionCaching) ? "db": "redis";

                public static int MinutesToLive = 1440;
                public static int DbExpireSessionTaskMilliSecDelay = 1800000;
            }

            /// <summary>
            /// Payment API credentials
            /// </summary>
            public static class PaymentApi
            {
                public const string LoginName = "php_gDpGBsM0r";
                public const string Password = "80NZBIfh9NGHYmt9Pk9sb1sQ";
            }
        }

        /// <summary>
        /// Cache Timeout defaults
        /// </summary>
        public static class CacheTimeouts
        {
            /// <summary>
            /// 5 Minutes
            /// </summary>
            public const int VeryShort = 5;
            /// <summary>
            /// 20 minutes
            /// </summary>
            public const int Short = 20;
            /// <summary>
            /// 2 Hours in minutes (120)
            /// </summary>
            public const int Long = 120;
            /// <summary>
            /// 1 Day in minutes (1440)
            /// </summary>
            public const int VeryLong = 1440;
        }

        /// <summary>
        /// Default backoffice settings
        /// </summary>
        public static class Backoffices
        {
            //MW 90863 08/14/17 changed idle timeout from 15 minutes to 120 per request
            public static int SessionTimeout = 120; // In minutes

            public const string CookieNameForSessionIDFromReplicatedSite = "ReplicatedSiteSession";

            public const string BackofficeProductionUrl = "http://backoffice.mypurium.com";    //#64947 Ivan S. 2015-11-25 to be able to have the Back Office prefix for production
            public const string BackofficeLocalUrl = "http://localhost:58777";
            public const string BackofficeUATUrl = "http://devphpbackoffice.azurewebsites.net";    //#64947 Ivan S. 2015-11-25 to be able to have the Back Office prefix for UAT
            public const string BackofficeEuropeUATUrl = "http://devphpboeu.azurewebsites.net";    //#64947 Ivan S. 2015-11-25 to be able to have the Back Office prefix for UAT
            /// <summary>
            /// Silent login URL's and configurations
            /// </summary>
            public static class SilentLogins
            {
                public static string DistributorBackofficeSilentLogin = "/silentlogin/?token={0}";    //#64947 Ivan S. 2015-11-25 I removed the prefix to be able to use the suffix of the URL for different environments
                public static string RetailCustomerBackofficeUrl = "http://www.company.com/account/silentlogin/?token={0}";
            }

            /// <summary>
            /// Waiting room configurations
            /// </summary>
            public static class WaitingRooms
            {
                /// <summary>
                /// The number of days a customer can be placed in a waiting room after their initial enrollment.
                /// </summary>
                public static int GracePeriod = 30; // In days
            }
        }

        /// <summary>
        /// Default replicated site settings
        /// </summary>
        public static class ReplicatedSites
        {
            public static string DefaultWebAlias = "www";
            public static int DefaultAccountID = 1;
            public static int IdentityRefreshInterval = 15; // In minutes
            //public const string ReplicatedSiteProductionUrl = "http://mypurium.com/"; //64947 Ivan S. 2015-11-25 Added the main URL for Production for future use
        }

     


        /// <summary>
        /// Customer avatar configurations
        /// </summary>
        public static class Avatars
        {
            public static string DefaultAvatarAsBase64 = "/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCAEsASwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD0WiiigAooooAKKKKACiiigBO1LRRQAneloooAKKKKAEpaKKACiiigApO1LRQAUUUUAFFFFABRRRQAUUUUAJ3paKKACiiigAoopO1AC0UUUAFFFFABRSd6WgAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAopKWgBOKWiigAooooAKKTvS0AJS0UUAFJS0UAFFFFABRRRQAUnelooAKKKKACiiigApO9LRQAnaloooAKKKKACiiigBKKWigAopO9LQAUlLRQAUUUUAFJS0UAJS0UUAFFFFACUtFFABSUtFABSUtJQAtFFFABRRRQAUUUUAFFFFABRRRQAUUUnegBaKKKACinRxvK4SNSzHsK17bQJGwbiTYP7q8mgDGpyo7/AHULfQV1cOl2cH3YQx9W5q4FVRhQB9KAOMFpdHpbSn/gBprW06fehkH1Q121FAHCkYNFdrJbwyjEkSN9VrPn0O1lyY90Te3IoA5qir11pVzaguV3oP4lqjQAUUneloAKKKKACiiigAooooAKKKKACiiigAooooAKKTvS0AFFFFABRRRQAUnalooAKKKKACiiigAq9YaZJetuOUiHVvX6U/S9NN3J5knEKn/vr2rp1VUUKoAUcADtQBFbWkNomyFMep7mp6KKACiiigAooooAKKKKACsq/wBHiuQXhAjl/Rq1aKAOIlieCQxyKVYdQaZXXX9hHfRYPEgHyt6Vyk0TwStHIuGWgBlFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFACd6WiigAooooAKKKKACk5paKACrFlaNeXKxDherH0FV66jRrUW9mJCP3kvzH6dqAL8USQxLGgwqjAFPoooAKKKKACikpaACiiigAooooAKKKKACszV7D7TD5qD96g7fxD0rTooA4Wir+r2gtbzKDCSfMPaqFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQBPZQ/abyOLszc/SuyAwMDpXO+H4g11JIf4VwPxro6ACiiigAooooAKKO1HagAooooAO9HakpaACiiigAooooAzdat/OsGcfejO4f1rl67eRBJE6HoykGuJZdrFT1BwaAEooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAoopO1AHQ+Hl/cTN6uBW1WP4e/49JR/00/pWxQAUUUUAFFFJ3oAWiiigAooooAKKKKACiiigAooooAK4y+XbfTr/tmuzrjtROdRuP8AfNAFaiiigAooooAKKKKACiiigAooooAKKKKACkpaKACiiigAooooAKKKKAN3w6/+vj+jVu1y2izeVqKqTxICtdTQAUUUUAFFJS0AFFFFABRRRQAUUUUAFFFFABRRRQAVxVw/mXMr/wB5yf1rrL6b7PZTSdwpx9a46gAooooAKKKKACiiigAooooAKKKKAE70tFFABRRRQAUUUnegBaKKKACik7UtACo5jkV14ZTkV2dvMLi3SVejDNcXW1oV7tc2rn5W5T60AdBRRRQAUUdqKACikpaACiiigAooooAO1FFFABRRUU0yQQtK5wqjJoAyNfucLHbA9fmb+lYNS3E73M7zP95j+VRUAFJ2paTvQAtFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFKrFWDA4I5BpKKAOq0zUFvIcMcTL94evvWhXEQzPBKssbbWXvXT6fqUV6m0kLMOq+v0oA0KKKKACiiigAooooAKKKKACiimswRSzEBR1JoAUkAZNczq+ofapPJiP7lD1/vGpNU1bz8wW5Ij6M396sigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACk70tFABRRRQAUUUnegBaKKKAClVmRgykhh0Iq5Z6XcXnzAbI/7zD+Vb1ppVta4bb5kn95qAINMvLudQs0DFe0vStaiigAopKWgAooooAKKKKAGSu0cZZELkdFB61y+o311cSFJlaJR/BjFdXUUsEc6bZUDr7igDiqK3bvQOr2r/APAG/wAaxZI3hcpIhVh2IoAZRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUVJBBJcSrHEu5jQAxEeRwiKWY8ACugsNFSLElzh36hewq3YadHZJnhpT95sfyq9QAgAAwKWiigAooooAKKSloAKKKKACiiigAooo7UAFV7mzhu02ypn0PcVYooA5K+02WybP34j0cD+dUq7hlV1KsAVPUGuc1PSTbZmgBMXcf3aAMqiiigAooooAKKKKACiiigAooooAKKKTtQAtFFFABRSUtABRRRgmgB8UTzSrHGu5m4ArqtPsUsYcDmRvvNUOk6eLWLzZB++f8A8dHpWnQAUUUd6ACjtRRQAUlFLQAUUUUAFFFFABRRRQAUUUUAFFFFABSEAjB6UtFAHNarpn2ZjNEP3THkf3ayq7h0WRCrgMp4INcnqNi1lcYHMbcqf6UAU6KKKACiiigAooooAKKKKACiik7UALRRRQAlFLRQAVr6LY+dL9okHyIflHqazLeB7idIU+8xxXY28K28CRIAFUYoAlooooAKKKKACiiigAooooAKO1FFABRRRQAUUUUAFFFFABRRRQAUUUUAFVr21S8tmibg9VPoas0UAcPJG0UjRuMMpwRTa3des+l0g9n/AKGsKgAooooAKKKKACiiigAooooAKKKKACiiljRpJFRclmOBQBu6BagK10w5Pyp/WtyoreFbeBIl6KMVLQAUUUUAFFFHagApKWigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAGSxrNE0bjKsMGuMuImt7h4m6qcV21c/4gttskdwo4b5W+tAGLRRRQAUUUUAFFFFACd6WiigAopKWgArT0O382+8wj5Yxu/HtWZXSaDDssmkPWRv0FAGtRR2ooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAqpqMH2mxlTGTjK/UVbooA4Wip72HyL2aPsG4+lQUAFFFFABRRRQAUUUUAFFFFABXZWUfk2UMfogz9a5GBPMuI0/vOBXa0ALRRRQAUlLRQAUUUUAFFFFABRRSUAHeloooAKKKKACiiigAo7UUUAFFFFABR2oooAKKKKACiiigDmtfjC3qPj76frWVXQeIY8wQyf3WxXP0AFFFFABRRRQAUUUUAFFJ2ooAuaWu/U4B/tZ/KuvrldF51SP2B/lXVUAFHaiigA70UlLQAUUd6O9ABRRRQAlLRR3oAKKKKACiiigAooooAKKKKACiiigAoo70UAFFFFABRRR3oAzdcXdpjn+6wNcvXW6tzpc/0H865KgAooooAKKTvS0Af//Z";
        }

        /// <summary>
        /// Error logging configuration
        /// </summary>
        public static class ErrorLogging
        {
            public static bool ErrorLoggingEnabled = true; //Enables Travis' logging system that catches the exception using the Error event of the Global.asax file
            public static string[] EmailRecipients = new string[] { "laurieg@exigo.com" };
        }

        /// <summary>
        /// EncryptionKeys used for silent logins and other AES encryptions
        /// </summary>
        public static class EncryptionKeys
        {
            public static string General = "SDFN0SF97FM09348590238M4"; // 24 characters 

            public static class SilentLogins
            {
                public static string Key = GlobalSettings.Exigo.Api.CompanyKey + "silentlogin";
                public static string IV = "m3jJ8sne9l1KH9wn"; // Must be 16 characters long
            }
        }

        /// <summary>
        /// Regular expressions used throughout all websites
        /// </summary>
        public static class RegularExpressions
        {
            public const string EmailAddresses = "[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
            public const string LoginName      = "^[a-zA-Z0-9]+$";
            public const string Password       = "^.{8,50}$"; //6/23 Ivan: Craig and I changed the minimum length to 8 characters 
            public const string WebAlias       = "(?=^.{6,25}$)(?=.*[a-zA-Z])[\\w-_]*$";
            public const string ZipCode        = "^[-A-z0-9\\s]+$";
            public const string FullName       = "[-',A-z]{1,}\\s[-',A-z]{1,}";
            public const string PhoneNumber    = @"^([\d\.()\+-][\s]?)+$";
            public const string AboutMe        = "^[^<>]*$";
            public const string RoutingNumber = "^[0-9]{9}$"; // M.N Ticket 82739 
            public const string AccountNumber = "^[0-9]{10,20}$";// M.N Ticket 81871  added Account number regex
            public const string Swift = "^([a-z]{4}[a-z]{2}[1-9a-z]{2}[a-z0-9]{3}){1}?"; // M.N Ticket 82739 
                                                                                         //J.M Ticket #67651 && #65594 (Issue #738 BO && Issue #739) changed phone regex to allow european numbers

        }

        public static class SiteSettings
        {
            public const int MobileMaxScreenSize = 992;
        }
    }

    public enum MarketName
    {
        UnitedStates,
        NonContingousUS,
        Canada,
        Europe,
        Germany, 
        UnitedKingdom,
        Austria,
        Albania,
        Andorra,
        Belgium,
        BosniaHerzegovina,
        Bulgaria,
        Croatia,
        Cyprus,
        CzechRepublic,
        Denmark,
        Estonia,
        FaroeIslands,
        Finland,
        France,
        Gibraltar,
        Greece,
        Greenland,
        Guernsey,
        Vatican,
        Hungary,
        Iceland,
        Ireland,
        Italy,
        Jersey,
        Latvia,
        Liechtenstein,
        Lithuania,
        Luxembourg,
        Macedonia,
        Malta,
        Monaco,
        Netherlands,
        Norway,
        Poland,
        Portugal,
        Romania,
        SanMarino,
        Scotland,
        SerbiaMontenegro,
        SlovakRepublic,
        Slovenia,
        Spain,
        SvalbardJanMayenIslands,
        Sweden,
        Switzerland,
        Turkey,
        Yugoslavia
    }    
    public enum AvatarType
    {
        Tiny,
        Small,
        Default,
        Large
    }
    public enum SocialNetworks
    {
        Facebook   = 1,
        GooglePlus = 2,
        Twitter    = 3,
        Blog       = 4,
        LinkedIn   = 5,
        MySpace    = 6,
        YouTube    = 7,
        Pinterest  = 8,
        Instagram  = 9
    }

    public static class CustomerStatusTypes
    {
        public const int Active = 1;
        public const int Terminated = 2;
        public const int Inactive = 3;

    }
    public static class NewsDepartments
    {
        public const int Backoffice = 7;
        public const int Replicated = 8;
        public const int GermanBackoffice = 11;
        public const int UKBackoffice = 12;
        public const int GermanReplicated = 13;
        public const int ReplicatedGB = 10;
        public const int ESBackoffice = 14;
        public const int ESReplicated = 15;
    }
    public static class AutoOrderPaymentTypes
    {
        public const int PrimaryCard     = 1;
        public const int SecondaryCard   = 2;
        public const int DebitChecking   = 3;
        public const int WillSendPayment = 4;
        public const int PrimaryWallet   = 6;
        public const int SecondaryWallet = 7;
        
    }

    public static class MarketLanguageID
    {
        public const int US_English = 1;
        public const int US_Spanish = 2;
        public const int EU_German = 3;
        public const int EU_English = 4;
    }

    //LA - 5/30/17 - 89019 - added new urls for tracking based off whether is a European country or specifically Germany
    public static class EUBaseUrls
    {
        public static string EU_Url = "https://www.post.at/sendungsverfolgung.php/details?pnum1=";
        public static string DE_Url = "https://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=de&idc=";
    }
    
}