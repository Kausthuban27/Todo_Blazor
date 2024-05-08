using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using Todo_Blazor.Model;

namespace Todo_Blazor.SharedService
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly UserState_Management_Service _userStateService;

        public UserService(HttpClient httpClient, UserState_Management_Service userStateService)
        {
            _httpClient = httpClient;
            _userStateService = userStateService;
        }
        public async Task<bool> LoginUserAsync(string username, string password)
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "GetUser";

                var queryBuilder = new QueryBuilder
                {
                    { "username", username },
                    { "password", password }
                };

                uriBuilder.Query = queryBuilder.ToString();
                var response = await _httpClient.GetAsync(uriBuilder.Uri);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "AddUser";

                var response = await _httpClient.PostAsJsonAsync(uriBuilder.Uri, user);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
