using AIMCPSample;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Nany.Shared;
using Nany.Shared.DBModel.Model.Cosmos;
using System.Net.Http.Json;

namespace Nany.Client.Controller
{
    public class CR_ControllerBase<T> : ControllerBase, IDisposable where T : EntityBase, new ()
    {
        #region Properties
        
        public virtual bool ToView { get; set; } = true;
        public virtual bool toShowPhoto { get; set; } = false;
        public virtual bool showOriPhoto { get; set; } = false;

        public string? SearchTitle { get; set; }
        public string? SearchKeyWord { get; set; }

        public string? SearchProiroty { get; set; }

        public bool ShowDialog { get; set; } = false;

        public bool ShowSubDialog { get; set; } = false;
        public string strInfo { get; set; }
        public bool ReadOnly { get; set; }
        public bool ManagementMode { get; set; } = true;

        protected virtual string PageTitle { get; set; } = String.Empty;
        public virtual string APIPath { get; set; } = String.Empty;

        protected virtual string SubjectForNewCreate { get; } = String.Empty;
        public virtual bool ExecutedResult { get; set; }

        public int? ContractQueryTimes { get; set; } = 1;
        public string? Prompts { get; set; }
        public string? AiMessage { get; set; }
 
        public string? SearchStatus { get; set; } = "";

        protected override bool ShouldRender() => PageStatus == EN_PageStatus.Inited;

        public virtual int IntPhotoWidth { get; set; } = 300;

        public string _AlertMessage { get; set; } = String.Empty;

        public DateTime? _AlertMessageTime { get; set; } = DateTime.Now;
        public string AlertMessage 
        { 
            get 
            {
                if ( ! string.IsNullOrEmpty( this._AlertMessage) && this._AlertMessageTime.Value.AddSeconds(5) < DateTime .Now)
                {
                    this._AlertMessage = string.Empty;
                }

                //if(!string.IsNullOrEmpty(this._AlertMessage))
                //{
                //    return this._AlertMessage + " @" +_AlertMessageTime.ToStandardStringYMDHMS();
                //}

                return this._AlertMessage;            
            } 
            set 
            {
                _AlertMessageTime = DateTime.Now;
                this._AlertMessage =value;
            } 
        } 

        public virtual T? RawObj { get; set; }

        public List<T> AllObject { get; set; }

        public int infoCount = 0;

        
        public EN_PageStatus _PageStatu { get; set; }

        public EN_PageStatus PageStatus
        {
            get { return this._PageStatu; }
            set
            {
                this._PageStatu=value;
            }
        }

        public EN_PageActions PageAction
        {
            get; set;
        }

        public virtual string? RawID { get { return (this.RawObj as EntityBase)?.ID; } }

        public System.Timers.Timer? aTimer { get; set; } = new System.Timers.Timer(5000) { Enabled = false };

        public bool IsWorking = false;

        public virtual bool ToAdd { get; set; } = false;
        public virtual bool ToUpdate { get; set; } = false;

        public virtual bool NeedToLogin { get; set; } = true;

        protected string? MainPageSelectedId { get; set; }
        public string? LinkedContractMoth { get; set; }
        protected virtual En_FunctionTypes FunctionType { get; set; }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if( this.MenuService._jsRuntime ==null)
                {
                    this.MenuService._jsRuntime = MyJSRuntime;
                }                

                this.PageStatus = EN_PageStatus.Initing;
                try
                {
                    await this.DoInitAsync();
                }
                catch { }


                try
                {
                    await this.DoInitAsync_Extend();
                }
                catch
                {

                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message + ex.StackTrace;
            }
            finally
            {
                this.PageStatus = EN_PageStatus.Inited;
            }
        }


  
        public virtual async Task<APIMSGReturn<T>> SearchAl(APIMSGSend aPIMSGSend)
        {
            var sqlCount = "SELECT VALUE COUNT(1) FROM c";
            var sql = "SELECT * FROM c";

            var where = "";



            if (!string.IsNullOrEmpty(where))
            {
                where = $"WHERE {where}";
            }

            sqlCount = $"{sqlCount} {where} "; //GROP BY c.ID

            sql = $"{sql} {where} ";

            if (!string.IsNullOrEmpty(aPIMSGSend.OrderBy))
            {
                sql = $"{sql} ORDER BY {aPIMSGSend.OrderBy} ";
            }
            else
            {
                sql += " ORDER BY c.tagObject.CreatedDatetime DESC";
            }


            aPIMSGSend.DirectCountSQL =sqlCount;
            aPIMSGSend.DirectSQL =sql;

            var s1 = await this.DoHTTPPostSearch(this.APIPath, aPIMSGSend);

            return s1;

        }

        public virtual async Task Show(string id = "", EN_PageActions n_PageAction = EN_PageActions.ToShowAdd)
        {

            this.ShowDialog = false;

            this.ExecutedResult = false;
            this.AlertMessage = String.Empty;
            this.ToUpdate = false;
            this.ToAdd = false;
            this.ReadOnly = false;

            this.PageAction = n_PageAction;

            if (n_PageAction== EN_PageActions.ToShowAdd)
                this.ToAdd = true;

            if (n_PageAction== EN_PageActions.ToShowUpdate)
                this.ToUpdate = true;

            if (string.IsNullOrEmpty(id))
            {

                this.RawObj = new T();
                // this.RawObj.EntityID = GlobalClient.CurrentUser.EntityID;
            }
            else
            {

                //this.ReadOnly = !this.CanUpdate;
                //  this.RawObj = await this.GetByIDAsync<EntityUserLogin>(this.APIPath, id);
            }

            this.ShowDialog = true;
            this.EnableModalBackdrop();

            this.StateHasChanged();

            await Task.CompletedTask;
        }


        public virtual async Task DoInitAsync()
        {
            this.RawObj = new();
            this.AllObject = new();
           

            if (this.NeedToLogin)
            {
                await this.InitForPage();
                MenuService!.NotifyStateChanged();
            }

            this.toShowPhoto = false;
            this.showOriPhoto = false;
        }


        public void Dispose()
        {
            //this.StopTimer();
        }

        public virtual string GetFilters()
        {
            return string.Empty;
        }

        public virtual async Task InitData()
        {
            await Task.CompletedTask;
        }

        public virtual async Task InitForPage()
        {
            try
            {
                if (GlobalClient.Config== null)
                {
                    var cf = new ConfigurationBuilder()
                        .AddJsonFile("Resources/appsettings.json")
                        .Build();

                    GlobalClient.Config = cf;
                    GlobalClient.RootPath = GlobalClient.Config["LLM:APIPath"] ?? "https://localhost:7222/";
                }

                //if (_MyCosmosService == null)
                //{
                //   CosmosDbSettings cosmosDbConfig = COSMOSSetting.Deserialize<CosmosDbSettings>();
                //   _MyCosmosService = CosmosDbService.InitializeCosmosClient(cosmosDbConfig);
                //}             
            }
            catch
            {
                //todo
            }


            await Task.CompletedTask;

        }
 
        protected virtual void GoHomePage()
        {
            MyNav!.NavigateTo("");
        }

 
        public virtual async Task GetAll(APIMSGSend? aPIMSGSend = null)
        {
            try
            {
                this.PageStatus = EN_PageStatus.Initing;

                this.AllObject.Clear();

                if (aPIMSGSend ==null)
                {
                    aPIMSGSend = new APIMSGSend();
                    aPIMSGSend.OrderBy = "c.updatedDate desc"; 
                }

                //aPIMSGSend.SkipCount = (this.CurrentPage - 1) * this.NoPerPage;
                //aPIMSGSend.TakeCount = this.NoPerPage;


                var s1 = await DoHTTPPostSearch(this.APIPath, aPIMSGSend);

              
                if (s1!= null && s1.Result!=null)
                {
                    //this.TotalCount = s1.TotalCount;
                    //this.PagesCount = (s1.TotalCount / this.NoPerPage);
                    //if (s1.TotalCount % this.NoPerPage > 0)
                    //{
                    //    this.PagesCount++;
                    //}
         
                    foreach (var inf in s1.Result)
                    {
                        try
                        {
                            var bd = inf.tagObject;
                            if (bd != null)
                            {
                                var entityBase = bd as EntityBase;
                                if (entityBase != null)
                                {
                                    entityBase.ID = inf.id;
                                }

                                AllObject.Add(bd);
                            }
                        }
                        catch
                        { }
                    }
                }

            }
            catch (Exception ex)
            {
                this.AlertMessage = (ex.Message + ex.StackTrace);

            }

            this.PageStatus = EN_PageStatus.Inited;
        }

        public async Task<APIMSGReturn<T>?> DoHTTPPostSearch(string apiPathRoot, APIMSGSend aPIMSGSend)
        {
            try
            {
                var requestMsg = new HttpRequestMessage(HttpMethod.Post, $"{apiPathRoot}/SearchAl");

                var bearer = await this.GetToken();
                requestMsg.Headers.Add("Authorization", "Bearer " + bearer);
                JsonContent content = JsonContent.Create(aPIMSGSend);
                requestMsg.Content = content;

                var response = await MyHttp!.SendAsync(requestMsg);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) //NOTE: THEN TOKEN HAS EXPIRED
                {
                    await this.ClearAsync();
                       MenuService!.NotifyStateChanged();
                }

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadFromJsonAsync<APIMSGReturn<T>>();
                    return res;

                }

                return default;
            }
            catch (Exception ex)
            {
                var ss = ex.Source + ex.StackTrace;

            }

            return default;
        }

        public virtual async Task DoInitAsync_Extend()
        {
            await Task.CompletedTask;
        }

        public virtual async Task<bool> Validate()
        {
            return true;
        }

        
       
        public virtual async Task<bool> AddAsync()
        {
            return await Task.FromResult(false);
        }
        public virtual async Task<bool> UpdateAsync()
        {
            return await Task.FromResult(false);
        }

        public virtual async Task<bool> UpdateObjectAsync(T rawObj)
        {
            return await Task.FromResult(false);
        }
 
        public virtual T? FindById(string id)
        {
            if (AllObject!=null && AllObject.Count>0)
            {
                var item = AllObject.FirstOrDefault(x => (x as EntityBase)?.ID == id);
                return item;
            }

            return default(T);
        }

 
 
 
        public virtual async Task DoSearchAndRefresh()
        {

            this.PageStatus = EN_PageStatus.Initing;
            try
            {
                //this.CurrentPage=1;

                //await RefreshPagesAsync();
            }
            catch (Exception ex) { }
            finally
            {
                this.PageStatus = EN_PageStatus.Inited;
                this.StateHasChanged();
            }


            await Task.CompletedTask;

        }

        public virtual async Task<bool?> ToClone(string id, string newEnitityID)
        {
 
            var rs = await this.HTTPClone(this.APIPath, id, newEnitityID.Trim());
            return rs; 
        }

        [Parameter]
        public virtual EventCallback CloseEventCallback { get; set; }

        public virtual bool NeedToRefreshParentPage { get; set; } = false;

        protected virtual async Task DoPopUpAdd()
        {
            await Task.CompletedTask;
        }


 
    }



 

}





