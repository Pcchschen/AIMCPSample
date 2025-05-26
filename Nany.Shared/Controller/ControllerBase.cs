using Nany;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nany.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
 
namespace Nany.Client.Controller
{
    public class ControllerBase : ComponentBase
    {
        [Inject]
        public MenuService? MenuService { get; set; }

        [Inject]
        public HttpClient? MyHttp { get; set; }

        [Inject]
        public NavigationManager? MyNav { get; set; }


        //[Inject]
        //public MyLocalStr? MyLocalStr { get; set; }


        //todo: remove this static variable
        static string Token { get; set; } = string.Empty;

        [Inject]
        public IJSRuntime? MyJSRuntime { get; set; }


        public async Task SaveToken(string token)
        {
            Token = token;

            await Task.CompletedTask;

            //await MyLocalStr!.
            // await MyJSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", "AuthToken", token);
        }

        public async Task<string> GetToken()
        {
            try
            {
               return await Task.FromResult(Token);

                //var token = await MyJSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "AuthToken");
                //return token;
            }
            catch(Exception ex)
            {
                //
            }

            return string.Empty;           
        }
 
       protected async Task  ClearAsync()
        {
            await SaveToken("");
        }

        public async Task<List<G>> GetAll<G>(String apiPointBase)
        {
            List<G> result = new();

            var s1 = await DoHTTPGet<IEnumerable<NanyItemBase<G>>>($"{apiPointBase}");

            if(s1 != null)
            {
                foreach (var inf in s1)
            {
                try
                {
                    var bd = inf.tagObject;

                    if (bd != null)
                    {
                        result.Add(bd);
                    }
                    else
                    {
                        if(inf.tagObject != null)
                        {
                            result.Add(inf.tagObject);
                        }
                    }
                }
                catch
                { }
            }
            }
            return result;
        }

    
        public async void EnableModalBackdrop()
        {
            try
            {
                await MyJSRuntime!.InvokeVoidAsync("disableScroll");
            }
            catch (Exception ex)
            {
            }
        }

        public async void DisableModalBackdrop()
        {
            try
            {
                await MyJSRuntime!.InvokeVoidAsync("enableScroll");
            }
            catch
            {

            }

        }

        protected virtual void GoToPage(string page)
        {          
            MyNav!.NavigateTo(page);
        }


        public async Task ResetUserInfo()
        {
            GlobalClient.ResetLoginInfo();

            await this.SaveToken("");

            MenuService!.NotifyStateChanged();
        }


        public async Task UpdateUserInfoAfterLogin(EntityUserLogin user)
        {
           
            //user.message = "Login successful.";
            //user.jwtBearer = CreateJWT(user);
            //user.success = true;
            if (user == null || user.success == false)
            {
                await this.ResetUserInfo();
                this.GoToPage(GlobalClient.PagePath_UserLogin);
            }
            else
            {
                await  this.SaveToken(user.jwtBearer!);
                GlobalClient.LoggedInDateTime = DateTime.Now;
                GlobalClient.CurrentUser = user;

               // await this.CheckRolePermission();

                if (GlobalClient.CurrentUser !=null )
                {
                    await this.GetAllEntityFunctions(GlobalClient.CurrentUser?.EntityID!);
                }


            }
        }


        public virtual async Task GetAllEntityFunctions(string entityId)
        {
            await Task.CompletedTask;
        }


        public async Task<R?> DoHTTPGet<R>(string url)
        {
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, url);
                var bearer = await this.GetToken();
                if(!string.IsNullOrEmpty(bearer))
                {
                    requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                }
               
                var response = await MyHttp!.SendAsync(requestMsg);

                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ClearAsync();
                    MenuService!.NotifyStateChanged();
                }

                if (response.IsSuccessStatusCode)
                {                    
                    var res = await response.Content.ReadFromJsonAsync<R>();
                    return res;
                }

            }
            catch (Exception ex) 
            {
              //todo            
            }

            return default(R);
        }
        public async Task<string?> HTTPGetString(string url)
        {
            HttpResponseMessage response = null;
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, url);
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);

                if (GlobalClient.CurrentUser !=null &&  !string.IsNullOrEmpty(GlobalClient.CurrentUser.EntityID))
                    requestMsg.Headers.Add("EntityID", GlobalClient.CurrentUser.EntityID);

                response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ClearAsync();
                    MenuService!.NotifyStateChanged();
                }

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return res;
                }

            }
            catch (Exception ex) { return ex.Message; }

            return string.Empty;
        }

        public async Task<HttpResponseMessage?> HTTPGet(string url)
        {
            HttpResponseMessage response = null;
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, url);
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ClearAsync();
                    MenuService!.NotifyStateChanged();
                }
            }
            catch (Exception ex) { }

            return response;
        }

        public async Task<bool?> HTTPClone(string url,string id, string newEntityID)
        {
          
            try
            {
                HttpResponseMessage response = null;
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, url+ $"/Clone/{id}/{newEntityID}");
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ResetUserInfo();
                       MenuService!.NotifyStateChanged();
                }
                else if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result == "1") 
                        return true;
                    else 
                        return false;
                }
                else  
                {
                    return false;
                }
            }
            catch (Exception ex) { }

            return true;
        }


        public async Task<HttpResponseMessage?> DoHTTPPOS<S>(string url, S obj)
        {
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Post, url);
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                JsonContent content = JsonContent.Create(obj);
                requestMsg.Content = content;

                var response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ResetUserInfo();
                    MenuService!.NotifyStateChanged();
                }

                return response;
            }
            catch (Exception ex)
            {
                var ss = ex.Source + ex.StackTrace;

            }

            return null;
        }




        public async Task<bool> DoHTTPUT<S>(string url, S obj, string id)
        {
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Put, $@"{url}\{id}");
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                JsonContent content = JsonContent.Create(obj);
                requestMsg.Content = content;
                var response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ResetUserInfo();
                       MenuService!.NotifyStateChanged();
                }
                else if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return true;
                }


            }
            catch (Exception ex) { }

            return true;
        }

        public async Task<bool> DoHTTDELETE(string url, string id)
        {
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Delete, $@"{url}\{id}");
                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                var response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ResetUserInfo();
                       MenuService!.NotifyStateChanged();
                }
                else if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return true;
                }


            }
            catch (Exception ex) {
                return false;
            }

            return true;
        }



    }
}




 