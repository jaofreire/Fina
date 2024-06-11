using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<FinaDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration["SqlServer:ConnectionStrings"]);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.MapPost("/", (CreateCategoryRequest modelRequest, ICategoryHandler handler)
    => handler.CreateAsync(modelRequest));



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.Run();

