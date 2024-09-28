using blog_backend.Repository;
using blog_backend.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//mapping the repository with the interface for usage
builder.Services.AddScoped<IUserRepository,UserRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.UseSwagger();
app.Run();
