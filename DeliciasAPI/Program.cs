using DeliciasAPI.Context;
using Microsoft.EntityFrameworkCore;
using DeliciasAPI.Services;
using DeliciasAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Inyecciones de dependencias
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMealService, MealService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IQuotesService, QuotesService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<ILoginService, LoginService>();

//Agregar el corsPolicy
builder.Services.AddCors(policyBulder =>
    policyBulder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
