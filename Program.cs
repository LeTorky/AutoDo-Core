using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                builder => builder
                    .WithOrigins("http://localhost:3000") // Add the origins you want to allow
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );
        });
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskDBContext>();
builder.Services.AddScoped<IUserRepository, UserRepositoryV1>();
builder.Services.AddScoped<ITaskRepository, TaskRepositoryV1>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseMiddleware<JWTAuthenticationMiddleware>();
app.UseMiddleware<GetOrCreateUserClaim>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
