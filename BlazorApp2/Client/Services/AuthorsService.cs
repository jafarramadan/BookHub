using BlazorApp2.Shared;
using System.Net.Http.Json;

namespace BlazorApp2.Client.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorModel>> GetAllAuthorsAsync();
    }
    public class AuthorsService : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AuthorModel>> GetAllAuthorsAsync()
        {
            
                return await _httpClient.GetFromJsonAsync<List<AuthorModel>>("api/author/getallauthors");
            
            
        }

    }
}
