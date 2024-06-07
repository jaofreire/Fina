using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Fina.Core.Models;

namespace Fina.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest modelRequest);
        Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest modelRequest);
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest modelRequest);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest modelRequest);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest modelRequest);
        
    }
}
