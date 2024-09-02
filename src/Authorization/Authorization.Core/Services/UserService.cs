using Authorization.Core.Interfaces;
using Authorization.Core.Models;

namespace Authorization.Core.Services
{
    public class UserService
    {
        private readonly IUserRequests _userRequest;

        public UserService(IUserRequests userRequest)
        {
            _userRequest = userRequest;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var response = await _userRequest.GetAllAsync();

            return response;
        }





        //private readonly HttpClient _httpClient;
        //private string BaseUrl = "https://localhost:4088";


        //public UserService()
        //{
        //    _httpClient = new HttpClient();
        //}

        //public async ValueTask<IList<User>> GetAllAsync()
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync($"{BaseUrl}/api/users/");

        //    response.EnsureSuccessStatusCode();

        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var users = JsonConvert.DeserializeObject<IList<User>>(responseBody);

        //    return users!;
        //}
    }
}