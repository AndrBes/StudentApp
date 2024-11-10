using StudentWebApp.Models.Group;
using StudentWebApp.Models.Student;
using System.Text.Json;

namespace StudentWebApp.Services
{
    public class StudentApiService
    {
        private HttpClient _httpClient;

        public StudentApiService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("StudentApi");
        }

        private JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private async Task<T?> GetResponseData<T>(HttpResponseMessage httpResponse)
        {
            var responseText = await httpResponse.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<T>(responseText, _jsonSerializerOptions);

            return responseData;
        }

        public async Task<List<StudentDto>?> GetStudents(StudentFilterDto filter)
        {
            var response = await _httpClient.PostAsJsonAsync("/Student/GetAll", filter);
            var responseData = await GetResponseData<List<StudentDto>?>(response);

            return responseData;
        }

        public async Task<List<GroupDto>?> GetGroups()
        {
            var response = await _httpClient.GetAsync($"/Group/GetAll");
            var responseData = await GetResponseData<List<GroupDto>>(response);

            return responseData;
        }
        public async Task<bool> RemoveStudent(int id)
        {
            var response = await _httpClient.DeleteAsync($"/Student/Delete?id={id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<StudentDto?> GetStudent(int id)
        {
            var response = await _httpClient.GetAsync($"/Student/Get?id={id}");
            var responseData = await GetResponseData<StudentDto?>(response);

            return responseData;
        }

        public async Task<bool> EditStudent(StudentEditDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/Student/Post", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddStudent(StudentAddDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("/Student/Add", dto);

            return response.IsSuccessStatusCode;
        }
    }
}
