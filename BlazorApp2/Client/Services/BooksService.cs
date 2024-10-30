using System.Net.Http.Json;
using BlazorApp2.Shared;
namespace BlazorApp2.Client.Services
{
    interface Ibook
    {
        Task<List<BookModel>> GetBooksAsync();
        Task DeleteBookAsync(int id);
        Task<BookModel> AddBookAsync(BookModel book);
        Task<BookModel> UpdateBookAsync(int id, BookModel book);
        Task<BookModel> GetBookByIdAsync(int id);
    }
    public class BooksService : Ibook
    {

        private readonly HttpClient _httpClient;

        public BooksService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookModel>> GetBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookModel>>("api/book/getallbooks");
        }

        public async Task<BookModel> AddBookAsync(BookModel book)
        {
            var response = await _httpClient.PostAsJsonAsync("api/book/addbook", book);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookModel>();
        }

        public async Task<BookModel> UpdateBookAsync(int id, BookModel book)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/book/updatebook/{id}", book);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookModel>();
        }

        public async Task DeleteBookAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/book/DeleteBook/{id}");
            //response.EnsureSuccessStatusCode();
        }
        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<BookModel>($"api/book/GetBookById/{id}");
            return response;
        }
    }
}