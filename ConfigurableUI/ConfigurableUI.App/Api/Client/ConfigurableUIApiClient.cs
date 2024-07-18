namespace ConfigurableUI.App.Api.Client
{
    using Blazored.LocalStorage;
    using ConfigurableUI.App.Api.Entities;
    using ConfigurableUI.App.Providers;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class ConfigurableUIApiClient
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorageService;

        public ConfigurableUIApiClient(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.authStateProvider = authStateProvider;
            this.localStorageService = localStorageService;
        }

        public Task<GetTemplateResponse?> GetTemplate()
        {
            return this.httpClient.GetFromJsonAsync<GetTemplateResponse>("/api/admin/template");
        }

        public Task SaveTemplate(SaveTemplateRequest request)
        {
            return this.httpClient.PostAsJsonAsync("/api/admin/template", request);
        }

        public Task<GetUserProfileResponse?> GetUserProfile()
        {
            return this.httpClient.GetFromJsonAsync<GetUserProfileResponse>("api/user/profile");
        }

        public Task SaveUserProfile(SaveUserProfileRequest request)
        {
            return this.httpClient.PostAsJsonAsync("api/user/profile", request);
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var loginResponseMessage = await this.httpClient.PostAsJsonAsync("api/auth/login", request);

            if (!loginResponseMessage.IsSuccessStatusCode)
            {
                return new LoginResponse(false, null);
            }

            var loginResponse = await loginResponseMessage.Content.ReadFromJsonAsync<LoginResponse>();

            if (!loginResponse.Success)
            {
                return loginResponse;
            }

            await this.localStorageService.SetItemAsStringAsync("authToken", loginResponse.Token);
            this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResponse.Token);
            ((AuthStateProvider)this.authStateProvider).NotifyUserAuthentication(loginResponse.Token);

            return loginResponse;
        }
    }
}
