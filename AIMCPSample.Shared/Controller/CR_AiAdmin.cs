using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using ModelContextProtocol.Client;
using Nany.Shared;
using OpenAI;
using AIMCPSample;


namespace Nany.Client.Controller
{
    public class CR_AIAdmin : CR_ControllerBase<EntityAi>
    {

        public IMcpClient mcpClient { get; set; } = null!;


        public override async Task GetAll(APIMSGSend? aPIMSGSend = null)
        {

            await Task.CompletedTask;

            //var tools = await mcpClient.ListToolsAsync();
            //foreach (var tool in tools)
            //{
            //    Console.WriteLine($"{tool.Name} ({tool.Description})");
            //}

        }


        public IList<ChatMessage> chatHistory { get; set; } = new List<ChatMessage>();

        public string Question { get; set; } = string.Empty;

        public override async Task DoInitAsync_Extend()
        {
            if (GlobalClient.ChatClient == null)
            {
                try
                {
                    var endpoint = new Uri(GlobalClient.Config["LLM:MCPEndPoint"]);
                    var sseOptions = new SseClientTransportOptions
                    {
                        Endpoint = endpoint
                    };
                    var transport = new SseClientTransport(sseOptions);

                    var options = new McpClientOptions();
                    // Set any valid properties here if needed

                    this.mcpClient = await McpClientFactory.CreateAsync(transport, options);



                    var apiKey = GlobalClient.Config["LLM:ApiKey"];
                    var modelId = GlobalClient.Config["LLM:ModelId"];

                    var openAIClient = new OpenAIClient(apiKey).AsChatClient(modelId);
                    var mcpTools = await mcpClient.ListToolsAsync();
                    GlobalClient.ChatOptions = new ChatOptions()
                    {
                        Tools = [.. mcpTools]
                    };

                    GlobalClient.ChatClient = new ChatClientBuilder(openAIClient)
                    .UseFunctionInvocation()
                    .Build();

                    //this.chatHistory.Add(new ChatMessage(ChatRole.System, "You are a helpful assistant delivering time in one sentence in a short format, like 'It is 10:08 in Paris, France."));

                }
                catch (Exception e)
                {
                    string ss = e.Message;
                    Console.WriteLine(e.Message + e.StackTrace);
                }
            }
        }


        public async Task AskAi()
        {
            this.PageStatus = EN_PageStatus.Initing;
            try
            {

                // 1. 构造系统消息，注入当前用户信息
                var sysMsg = new ChatMessage(
                    ChatRole.System,
                    $"The current logged-in user info is: {System.Text.Json.JsonSerializer.Serialize(GlobalClient.CurrentUser)}. " +
                    "When a tool requires an EntityCurrentUser parameter, use this object."
                );

                // 2. 构造临时对话历史
                var tempHistory = new List<ChatMessage>(chatHistory)
                {
                    sysMsg,
                    new ChatMessage(ChatRole.User, this.Question)
                };

                // 3. 调用 AI
                var response = await GlobalClient.ChatClient.GetResponseAsync(tempHistory, GlobalClient.ChatOptions);
                var content = response.ToString();

                // 4. 更新历史
                chatHistory.Add(new ChatMessage(ChatRole.User, this.Question));
                chatHistory.Add(new ChatMessage(ChatRole.Assistant, content));
            }
            catch (Exception ex)
            {
                //Method not found: 'System.String Microsoft.Extensions.AI.ChatResponse.get_ChatThreadId()'.
                chatHistory.Add(new ChatMessage(ChatRole.Assistant, ex.Message));
            }
            finally
            {
                this.PageStatus = EN_PageStatus.Inited;
            }
        }

       
    }



}



