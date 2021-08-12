namespace CommunityApi.Services
{
    using System.Threading.Tasks;
    using CommunityApi.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;

    public class Foo : IDbService
    {

        private readonly ILogger _logger;

        public Foo(ILogger<IDbService> logger)
        {
            this._logger = logger;
            this._commContainer = dbClient.GetContainer(databaseName, commContainerName);
            this._userContainer = dbClient.GetContainer(databaseName, userContainerName);
            this._discussionContainer = dbClient.GetContainer(databaseName, discussionContainerName);
        }

        // ============================
        // User CRUD
        // ============================
        public async Task AddUserAsync(CommunityApi.Models.User u)
        {
            await this._userContainer.CreateItemAsync<CommunityApi.Models.User>(u, new PartitionKey(u.id));
        }

        public async Task<List<CommunityApi.Models.User>> GetUsersAsync(string field, string contains)
        {
            // Embed string in field name ergh hate this, but CosmosDB can' parametrize field names
            QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE contains(lower({field}), @contains)").WithParameter("@contains", contains.ToLower());
            var iterator = this._userContainer.GetItemQueryIterator<CommunityApi.Models.User>(query);
            List<CommunityApi.Models.User> results = new List<CommunityApi.Models.User>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<CommunityApi.Models.User> GetUserAsync(string id)
        {
            try
            {
                ItemResponse<CommunityApi.Models.User> resp = await this._userContainer.ReadItemAsync<CommunityApi.Models.User>(id, new PartitionKey(id));
                return resp.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<System.Net.HttpStatusCode> DeleteUserAsync(string id)
        {
            try
            {
                ItemResponse<CommunityApi.Models.User> resp = await this._userContainer.DeleteItemAsync<CommunityApi.Models.User>(id, new PartitionKey(id));
                return System.Net.HttpStatusCode.NoContent;
            }
            catch (CosmosException ex)
            {
                return ex.StatusCode;
            }
        }

        public async Task<CommunityApi.Models.User> UpdateUserAsync(string id, CommunityApi.Models.User user)
        {
            ItemResponse<CommunityApi.Models.User> resp = await this._userContainer.UpsertItemAsync<CommunityApi.Models.User>(user, new PartitionKey(id));
            return user;
        }

        // ============================
        // Community CRUD
        // ============================
        public async Task AddCommunityAsync(Community comm)
        {
            await this._commContainer.CreateItemAsync<Community>(comm, new PartitionKey(comm.id));
        }

        public async Task<List<Community>> GetCommunitiesAsync(string field, string contains)
        {
            // Embed string in field name ergh hate this, but CosmosDB can' parametrize field names
            QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE contains(lower({field}), @contains)").WithParameter("@contains", contains.ToLower());
            var iterator = this._commContainer.GetItemQueryIterator<Community>(query);
            List<Community> results = new List<Community>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Community> GetCommunityAsync(string id)
        {
            try
            {
                ItemResponse<Community> resp = await this._commContainer.ReadItemAsync<Community>(id, new PartitionKey(id));
                return resp.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<System.Net.HttpStatusCode> DeleteCommunityAsync(string id)
        {
            try
            {
                ItemResponse<Community> resp = await this._commContainer.DeleteItemAsync<Community>(id, new PartitionKey(id));
                return System.Net.HttpStatusCode.NoContent;
            }
            catch (CosmosException ex)
            {
                return ex.StatusCode;
            }
        }

        public async Task<Community> UpdateCommunityAsync(string id, Community comm)
        {
            ItemResponse<Community> resp = await this._commContainer.UpsertItemAsync<Community>(comm, new PartitionKey(id));
            return comm;
        }

        // ============================
        // Discussion CRUD
        // ============================
        public async Task AddDiscussionAsync(Discussion discussion)
        {
            await this._discussionContainer.CreateItemAsync<Discussion>(discussion, new PartitionKey(discussion.id));
        }

        public async Task<List<Discussion>> GetDiscussionsAsync(string field, string contains)
        {
            // Embed string in field name ergh hate this, but CosmosDB can' parametrize field names
            QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE contains(lower({field}), @contains) ORDER BY c.created DESC").WithParameter("@contains", contains.ToLower());
            var iterator = this._discussionContainer.GetItemQueryIterator<Discussion>(query);
            List<Discussion> results = new List<Discussion>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Discussion> GetDiscussionAsync(string id)
        {
            try
            {
                ItemResponse<Discussion> resp = await this._discussionContainer.ReadItemAsync<Discussion>(id, new PartitionKey(id));
                return resp.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<System.Net.HttpStatusCode> DeleteDiscussionAsync(string id)
        {
            try
            {
                ItemResponse<Discussion> resp = await this._discussionContainer.DeleteItemAsync<Discussion>(id, new PartitionKey(id));
                return System.Net.HttpStatusCode.NoContent;
            }
            catch (CosmosException ex)
            {
                return ex.StatusCode;
            }
        }

        public async Task<Discussion> UpdateDiscussionAsync(string id, Discussion discussion)
        {
            ItemResponse<Discussion> resp = await this._discussionContainer.UpsertItemAsync<Discussion>(discussion, new PartitionKey(id));
            return discussion;
        }
    }
}