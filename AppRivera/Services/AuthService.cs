using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppRivera.Models;
using Newtonsoft.Json;


namespace AppRivera.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService()
        {
            /*_httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //_httpClient.BaseAddress = new Uri("http://jbcprogramming.somee.com/");*/


            _httpClient = new HttpClient();

        }
        public async Task<string> LoginAsync(string correo, string clave)
        {
            var login = new LoginRequest
            {
                Correo = correo,
                Clave=clave
            };


            var url = "http://jbcprogramming.somee.com/api/Acceso/Login";
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                return loginResponse?.Token;
            }

            /*var json = JsonSerializer.Serialize(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://jbcprogramming.somee.com/api/Acceso/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
                return loginResponse?.Token;
            }*/
            return null;
        }
    }
}
