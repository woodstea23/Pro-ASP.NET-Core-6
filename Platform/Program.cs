var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// The .Use method registers a middleware component in the request pipeline.
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get
        && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
});

app.UseMiddleware<Platform.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
