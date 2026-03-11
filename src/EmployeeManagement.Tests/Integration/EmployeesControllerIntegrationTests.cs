using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmployeeManagement.Tests.Integration
{
    public class EmployeesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EmployeesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetEmployees_Returns_OK_For_Admin()
        {
            var client = _factory.CreateClient();

            // Login as seeded admin
            var login = new { Username = "admin", Password = "Admin123!" };
            var resp = await client.PostAsync("/api/auth/login", new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();

            var content = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);
            var token = doc.RootElement.GetProperty("token").GetString();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var employeesResp = await client.GetAsync("/api/employees");
            Assert.True(employeesResp.IsSuccessStatusCode);
        }
    }
}
