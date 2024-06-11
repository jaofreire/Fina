using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly FinaDbContext _dbContext;

        public CategoryHandler(FinaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest modelRequest)
        {
            try
            {
                var query = _dbContext.Categories
               .AsNoTracking()
               .Where(x => x.UserId == modelRequest.UserId)
               .OrderBy(x => x.Title);

                var categories = await query
                    .Skip(modelRequest.PageSize * (modelRequest.PageNumber - 1))
                    .Take(modelRequest.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(categories, count, modelRequest.PageNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);

                return new PagedResponse<List<Category>>(null, 500, "There is not possible get the categories");
            }

        }
        public async Task<Response<Category?>> GetByIdAsync(GetByIdCategoryRequest modelRequest)
        {
            try
            {
                var category = _dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);
              
                return category == null
                     ? new Response<Category?>(null, 404, $"The category with Id: {modelRequest.Id} not exist")
                     : new Response<Category?>(category, 200, "Category gets successfully");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                return new Response<Category?>(null, 500, $"There is not possible found the category with Id: {modelRequest.Id}");
            }
        }
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest modelRequest)
        {
            try
            {
                var newCategory = new Category()
                {
                    Title = modelRequest.Title,
                    Description = modelRequest.Description,
                    UserId = modelRequest.UserId
                };

                await _dbContext.Categories.AddAsync(newCategory);
                await _dbContext.SaveChangesAsync();

                return new Response<Category?>(newCategory, 201, "Category created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                return new Response<Category?>(null, 500, "There is not possible create the category");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest modelRequest)
        {
            try
            {
                var categoryUpdate = _dbContext.Categories
                    .FirstOrDefault(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);

                if(categoryUpdate == null)
                   return new Response<Category?>(null, 404, $"The category with Id: {modelRequest.Id} not exist");

                categoryUpdate.Title = modelRequest.Title;
                categoryUpdate.Description = modelRequest.Description;

                _dbContext.Categories.Update(categoryUpdate);
                await _dbContext.SaveChangesAsync();

                return new Response<Category?>(categoryUpdate, 200, "Category updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);

                return new Response<Category?>(null, 500, "There is not possible update the category");
            }
           
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest modelRequest)
        {
            try
            {
                var category = _dbContext.Categories
                    .FirstOrDefault(x => x.Id == modelRequest.Id && x.UserId == modelRequest.UserId);

                if (category == null)
                    return new Response<Category?>(null, 404, $"The category with Id: {modelRequest.Id} not exist");

                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();

                return new Response<Category?>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                return new Response<Category?>(null, 500, "There is not possible delete the category");
            }
        }




    }
}
