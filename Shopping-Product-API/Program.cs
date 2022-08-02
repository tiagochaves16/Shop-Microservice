using AutoMapper;
using Shopping_Product_API.Config;
using Shopping_Product_API.Model.Context;
using Shopping_Product_API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<BaseContext>(
    builder.Configuration["ConnectionString:ShopMicroservice"]);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository,ProductRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
