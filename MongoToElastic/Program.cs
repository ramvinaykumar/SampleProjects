using MongoToElastic.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger();

var app = builder.Build();
if (!app.Environment.IsEnvironment("Production"))
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCustomSwagger();
app.UseAuthorization();
app.UseCors("AllowCorsPolicy");
app.MapControllers();

app.Run();
