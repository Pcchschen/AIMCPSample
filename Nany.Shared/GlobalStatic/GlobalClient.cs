using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Nany.Shared;
using Nany.Shared.Model;

namespace Nany
{
    public static class GlobalClient
    {
        public static string? PageTitle { get; set; }
        public static List<EntityRole>? Roles { get; set; }

        public static string? ClientName { get; set; }
 
        public static DateTime LoggedInDateTime { get; set; }
        public static EntityRole? UserPermissionGroup { get; set; }
        public static EntityUserLogin? CurrentUser { get; set; }

        public static String? SelectedContractHisID { get; set; }

        public static void ResetLoginInfo()
        {
            Roles = null;
            LoggedInDateTime = DateTime.Now.AddYears(-100);
            UserPermissionGroup = null;
            CurrentUser = null;
        }

        public static IConfigurationRoot? Config { get; set; }

        public static ChatOptions? ChatOptions { get; set; }

        public static IChatClient? ChatClient { get; set; }


        public static int NoPerPage = 50;
        public static string Unauthorized = "验证失败，请登录！";
        public static string RootPath { get; set; }=@"https://localhost:7222/";
 
        /// <summary>
        /// api/UserLogin
        /// </summary>
        public static string APIPath_UserLogin => RootPath+ "api/UserLogin";

        public static string PagePath_UserLogin => "UserLogin";

    }
 
}
 
