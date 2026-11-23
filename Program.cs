using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();
    await next(context);
    stopwatch.Stop();

    app.Logger.LogInformation(
        "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs} ms",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds);
});

app.UseAuthorization();

app.MapControllers();

app.Run();

