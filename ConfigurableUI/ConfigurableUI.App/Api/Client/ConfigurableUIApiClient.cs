namespace ConfigurableUI.App.Api.Client
{
    using ConfigurableUI.App.Api.Entities;
    using System.Net.Http.Json;

    public class ConfigurableUIApiClient
    {
        private readonly HttpClient httpClient;

        public ConfigurableUIApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
    }
}
