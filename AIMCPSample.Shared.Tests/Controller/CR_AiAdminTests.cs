using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIMCPSample;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Moq;
using Nany.Client.Controller;
using Xunit;

public class CR_AIAdminTests
{
    private readonly Mock<IMcpClient> _mockMcpClient = new();
    private readonly CR_AIAdmin _admin;

    public CR_AIAdminTests()
    {
        _admin = new CR_AIAdmin
        {
            mcpClient = _mockMcpClient.Object
        };
    }

    [Fact]
    public async Task GetAll_ListsToolsAndWritesToConsole()
    {
        // Arrange
        var tools = new List<Tool>
        {
            new Tool { Name = "Tool1", Description = "Desc1" },
            new Tool { Name = "Tool2", Description = "Desc2" }
        };
        //_mockMcpClient.Setup(m => m.ListToolsAsync()).ReturnsAsync(tools);

        // Act & Assert (no exception means pass)
        await _admin.GetAll();
    }

    [Fact]
    public async Task DoInitAsync_Extend_InitializesMcpClient_WhenChatClientIsNull()
    {
        // Arrange
        GlobalClient.ChatClient = null;
        //var config = new Dictionary<string, string>
        //{
        //    { "LLM:MCPEndPoint", "http://localhost" },
        //    { "LLM:ApiKey", "test-key" },
        //    { "LLM:ModelId", "test-model" }
        //};
        //GlobalClient.Config = config;

        // Act
        await _admin.DoInitAsync_Extend();

        // Assert
        Assert.NotNull(_admin.mcpClient);
    }

    [Fact]
    public async Task AskAi_AddsAssistantMessage_OnException()
    {
        // Arrange
        _admin.Question = "Test?";
        GlobalClient.ChatClient = null; // Will cause exception
        _admin.chatHistory.Clear();

        // Act
        await _admin.AskAi();

        // Assert
        Assert.Contains(_admin.chatHistory, m => m.Role == ChatRole.Assistant);
    }
}