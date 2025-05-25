using Nany.Shared;
using AIMCPSample;

namespace Nany.Client.Controller
{
    public class CR_UserLoginAll : CR_ControllerBase<EntityUserLogin>
    {
        public override string APIPath { get; set; } = GlobalClient.APIPath_UserLogin; 

        protected override async Task DoPopUpAdd()
        {
            //TODO 
            await Task.CompletedTask;
        }

        protected virtual async Task DoPopUpUpdate(string id)
        {
            //TODO 
            await Task.CompletedTask;
        }        

        public override async Task DoInitAsync_Extend()
        {
            this.PageTitle ="登录账户管理";

            this.FunctionType = En_FunctionTypes.FC_1_UserLoginAccountAdmin;            
            await this.GetAll();
        }

    }

}



