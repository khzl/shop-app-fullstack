using Shop.Dtos.Login;
using Shop.Dtos.Register;
using Shop.Dtos.Tokens;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Wpf.UI.ClientServices.Interfaces;

namespace Wpf.UI.ClientServices.ClientService
{
    public class AuthClientService : IAuthClientService
    {
        // Properties 
        private readonly HttpClient _http;


        // public Constructor to initialize the HttpClient 
        public AuthClientService(HttpClient http)
        {
            _http = http;
        }

        // Feature 1  => (Login) 
        public async Task<LoginResponseDto> LoginAsync(string? email, string? password)
        {
            var response = await _http.PostAsJsonAsync(
                "api/Auth/Login",
                new LoginRequestDto
                {
                    Email = email,
                    Password = password
                });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (result == null)
            {
                throw new HttpRequestException("The server returned an empty response.");
            }

            return result;
        }

        // Feature 2 =>  (Register) 
        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "api/auth/register", // EndPoint for Register 
                dto
             );

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();

            if (result == null)
            {
                throw new HttpRequestException("The server returned an empty response.");
            }

            return result;
        }

        // Feature 3 => Logout + Token Handling
        public async Task LogoutAsync()
        {
            await _http.PostAsJsonAsync(
                "api/auth/logout",
                new LogoutRequestDto
                {
                    RefreshToken = TokenStore.RefreshToken
                });

            TokenStore.Clear();
        }

    }
}
