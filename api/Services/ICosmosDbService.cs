namespace CommunityApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommunityApi.Models;

    public interface ICosmosDbService
    {
        // User CRUD
        Task<List<User>> GetUsersAsync(string field, string contains);
        Task<User> GetUserAsync(string id);
        Task AddUserAsync(User user);
        Task<User> UpdateUserAsync(string id, User user);
        Task<System.Net.HttpStatusCode> DeleteUserAsync(string id);

        // Community CRUD
        Task<List<Community>> GetCommunitiesAsync(string field, string contains);
        Task<Community> GetCommunityAsync(string id);
        Task AddCommunityAsync(Community community);
        Task<Community> UpdateCommunityAsync(string id, Community community);
        Task<System.Net.HttpStatusCode> DeleteCommunityAsync(string id);

        // Discussion CRUD
        Task<List<Discussion>> GetDiscussionsAsync(string field, string contains);
        Task<Discussion> GetDiscussionAsync(string id);
        Task AddDiscussionAsync(Discussion discussion);
        Task<Discussion> UpdateDiscussionAsync(string id, Discussion discussion);
        Task<System.Net.HttpStatusCode> DeleteDiscussionAsync(string id);        
    }
}