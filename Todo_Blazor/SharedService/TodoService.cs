using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using Todo_Blazor.Model;

namespace Todo_Blazor.SharedService
{
    public class TodoService
    {
        private readonly HttpClient _httpClient;
        private readonly UserState_Management_Service _userStateService;

        public TodoService(HttpClient httpClient, UserState_Management_Service userStateService)
        {
            _httpClient = httpClient;
            _userStateService = userStateService;
        }

        public async Task<List<TodoData>> GetTasksAsync()
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "GetTasks/";

                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                queryParams["username"] = _userStateService.Username;
                uriBuilder.Query = queryParams.ToString();
                var response = await _httpClient.GetFromJsonAsync<List<TodoData>>(uriBuilder.Uri);
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddTasksAsync(TodoData todoData)
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "AddTasks";

                var response = await _httpClient.PostAsJsonAsync(uriBuilder.Uri, todoData);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTask(TodoData todoData)
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "UpdateTask";

                var response = await _httpClient.PutAsJsonAsync(uriBuilder.Uri, todoData);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch(Exception) 
            { 
                return false; 
            }
        }

        public async Task<bool> DeleteAsync()
        {
            try
            {
                var baseUri = _httpClient.BaseAddress!.ToString();
                if (!baseUri.EndsWith("/"))
                {
                    baseUri += "/";
                }

                UriBuilder uriBuilder = new UriBuilder(baseUri);
                uriBuilder.Path += "RemoveCompletedTasks/";

                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                queryParams["username"] = _userStateService.Username;
                uriBuilder.Query = queryParams.ToString();
                var response = await _httpClient.DeleteAsync(uriBuilder.Uri);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
