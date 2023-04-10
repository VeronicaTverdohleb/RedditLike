using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int todoid = 1;
        if (context.Posts.Any())
        {
            todoid = context.Posts.Max(todo => todo.Id);
            todoid++;
        }

        post.Id = todoid;
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    


    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Posts.Where(todo =>
                todo.Owner.UserName.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParameters.UserId);
        }
        

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existing = context.Posts.FirstOrDefault(todo => todo.Id == todo.Id);
        if (existing == null)
        {
            throw new Exception($"Post with id {post.Id} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(post);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
       
    }

    public Task<Post> GetByIdAsync(int id)
    {
        
        Post? existing = context.Posts.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(existing);
    }
    
    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(todo => todo.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    } 
}