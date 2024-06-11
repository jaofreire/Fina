using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Fina.Core.Models;

namespace Fina.Api.EndPoints.Categories
{
    public class CreateCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder builder)
            => builder.MapPost("/", HandlerAsync)
            .WithName("Categories - Create")
            .Produces<Response<Category>>();
       
        private static async Task<IResult> HandlerAsync(ICategoryHandler handler, CreateCategoryRequest modelRequest)
        {
            var response =  await handler.CreateAsync(modelRequest);

            return response.IsSuccess
                ? TypedResults.Created($"/v1/category/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
