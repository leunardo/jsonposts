using JsonPosts.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace JsonPosts.Services
{
    internal class JsonPlaceholderService
    {
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"),
        };

        /// <summary>
        /// Gets the posts from the JsonPlaceholder service.
        /// </summary>
        /// <returns>Enumerable of posts.</returns>
        public Task<IEnumerable<Post>?> GetPostsAsync()
        {
            return httpClient.GetFromJsonAsync<IEnumerable<Post>>("posts");
        }
    }
}
