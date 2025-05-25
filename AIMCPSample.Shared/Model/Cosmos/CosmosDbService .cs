using Microsoft.Azure.Cosmos;


namespace Nany.Shared.DBModel.Model.Cosmos
{ 
    public class CosmosDbService
    {
        public static  CosmosDbService InitializeCosmosClient(CosmosDbSettings cosmosDbConfig)
        {
            var cosmosDbService = new CosmosDbService();
            try
            {
                if (cosmosDbConfig!=null)
                {
                    cosmosDbService.Init(cosmosDbConfig.DatabaseName, cosmosDbConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return cosmosDbService;
        }

        public void  Init(string databaseName,CosmosDbSettings cosmosDbConfig)
        {
            AppConfig = cosmosDbConfig;
            this.DatabaseName = databaseName;
        }

        public Container GetContainer(string nameStr)
        {
            if(CosmosClient==null)
            {
                var options = new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway };
                CosmosClient = new CosmosClient(AppConfig?.EndpointUrl, AppConfig?.PrimaryKey, options);
            }

            var container = CosmosClient.GetContainer(DatabaseName, nameStr);

            return container;
        }

        private CosmosClient? CosmosClient { get; set; }
        private string? DatabaseName { get; set; }
        public CosmosDbSettings? AppConfig { get; set; }
    }

    public class ContainerData
    {
        public Container? Container { get; set; }

        public ContainerInfo? Info { get; set; }

    }

}
