using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", dto);
         if (!response.IsSuccessStatusCode)
         {
             string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
         
        
    }
    
    public async Task<ICollection<Post>> GetAsync(string? userName, int? userId, string? bodyContains, string? titleContains)
    {
        string query = ConstructQuery(userName, userId, bodyContains, titleContains);
        HttpResponseMessage response = await client.GetAsync("/posts"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> todos = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return todos;
    }
    
    private static string ConstructQuery(string? userName, int? userId, string? bodyContains, string? titleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (userId != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userid={userId}";
        }

        if (!string.IsNullOrEmpty(bodyContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"bodycontains={bodyContains}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }

        return query;
    }
    public async Task UpdateAsync(PostUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/posts", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/posts/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        PostBasicDto todo = JsonSerializer.Deserialize<PostBasicDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return todo;
    }
    
    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"posts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}
