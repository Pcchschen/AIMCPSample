using Microsoft.JSInterop;
using Nany.Shared;
using System.Net.Http.Json;
using AIMCPSample;
using System.Threading.Tasks;

namespace Nany.Client.Controller
{
    public class CR_Home : CR_ControllerBase<EntityUserLogin>
    {
       // public Dictionary<int, bool> dicExpandSubNavSettings = new Dictionary<int, bool>(10);

        protected override string PageTitle { get; set; } = "Login";
 
        public override async Task DoInitAsync_Extend()
        {
            await Task.CompletedTask;
        }        

        public async Task DoLogin()
        {
            try
            {
                this.PageStatus = EN_PageStatus.NotInited;

                EntityUserLogin entityUserLogin = this.RawObj!;

                var rs = await MyHttp!.PostAsJsonAsync<EntityUserLogin>($"{GlobalClient.APIPath_UserLogin}/Login", entityUserLogin);
                if (rs.IsSuccessStatusCode)
                {
                    var returnValue = await rs.Content.ReadFromJsonAsync<EntityUserLogin>();

                    if (returnValue != null && returnValue.success == true)
                    {
                        var user = returnValue.Clone();

                        await this.UpdateUserInfoAfterLogin(user);

                        this.MenuService.NotifyStateChanged();

                        //this.GoToPage("");
                    }
                    else
                    {
                        await this.ResetUserInfo();

                        this.AlertMessage = "用户名或密码错误，请检查!";
                    }

                    this.PageStatus = EN_PageStatus.Inited;
                }
                else
                {
                    await this.ResetUserInfo();

                    this.AlertMessage = "用户名不存在或密码错误，请检查!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }


    }
}
