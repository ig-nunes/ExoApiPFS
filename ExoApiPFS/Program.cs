using ExoApiPFS.Contexts;
using ExoApiPFS.Interfaces;
using ExoApiPFS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicyProjeto", policy =>
    {
        policy.WithOrigins("Http://localhost:7011")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddScoped<ExoApiContext, ExoApiContext>();

builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicyProjeto");

app.UseAuthorization();

app.MapControllers();

app.Run();
