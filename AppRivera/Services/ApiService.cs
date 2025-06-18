using AppRivera.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRivera.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        //public async Task<ApiResponse> LoginAsync(string correo, string clave)
        //{
        //    var response = await _client.PostAsync("https://jbcprogramming.somee.com/api/Acceso/Login",
        //        new StringContent(JsonConvert.SerializeObject(new { Correo = correo, Clave = clave }), Encoding.UTF8, "application/json"));

        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ApiResponse>(content);
        //}

        ////private static readonly HttpClient _client = new HttpClient();
        //private const string BaseUrl = "https://jbcprogramming.somee.com/api/Acceso/";
        //public async Task<ApiResponse> LoginAsync(string correo, string clave)
        //{
        //    var json = JsonConvert.SerializeObject(new { Correo = correo, Clave = clave });
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await _client.PostAsync($"{BaseUrl}Login", content);
        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return new ApiResponse
        //        {
        //            Success = false,
        //            //Message = $"Error del servidor: {response.StatusCode}"
        //        };
        //    }

        //    try
        //    {
        //        return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
        //    }
        //    catch
        //    {
        //        return new ApiResponse
        //        {
        //            Success = false,
        //            //Message = "Error al interpretar la respuesta del servidor."
        //        };
        //    }
        //}

        public async Task<bool> RegisterAsync(User model)
        {
            var url = "https://jbcprogramming.somee.com/api/Acceso/Registrarse";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }





        //public async Task<ApiResponse> RegisterAsync(User user)
        //{
        //    var response = await _client.PostAsync("https://jbcprogramming.somee.com/api/Acceso/Registrarse",
        //        new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ApiResponse>(content);
        //}
    }
}
