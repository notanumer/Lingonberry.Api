﻿using McMaster.Extensions.CommandLineUtils;
using Lingonberry.Api.Web.Commands;

namespace Lingonberry.Api.Web;

/// <summary>
/// Entry point class.
/// </summary>
[Command(Name = "lingonberry")]
[Subcommand(typeof(CreateUser))]
internal sealed class Program
{
    private static WebApplication? app;

    /// <summary>
    /// Entry point method.
    /// </summary>
    /// <param name="args">Program arguments.</param>
    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services, builder.Environment);
        app = builder.Build();
        startup.Configure(app, app.Environment);

        // Command line processing.
        var commandLineApplication = new CommandLineApplication<Program>();
        using var scope = app.Services.CreateScope();
        commandLineApplication
            .Conventions
            .UseConstructorInjection(scope.ServiceProvider)
            .UseDefaultConventions();
        return await commandLineApplication.ExecuteAsync(args);
    }

    /// <summary>
    /// This options is there to allow running the application with `--urls` parameter.
    /// https://paketo.io/docs/reference/dotnet-core-reference/#self-contained-deployment-and-framework-dependent-executables.
    /// </summary>
    [Option]
    public string? Urls { get; }

    /// <summary>
    /// Command line application execution callback.
    /// </summary>
    /// <returns>Exit code.</returns>
    public async Task<int> OnExecuteAsync()
    {
        if (app == null)
        {
            throw new InvalidOperationException("app is not initialized");
        }

        await app.InitAsync();
        await app.RunAsync();
        return 0;
    }
}
