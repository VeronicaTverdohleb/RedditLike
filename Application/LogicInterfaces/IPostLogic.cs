using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(PostUpdateDto post);
    Task DeleteAsync(int id);
    Task<PostBasicDto> GetByIdAsync(int id);
 
}