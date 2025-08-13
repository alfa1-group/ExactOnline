using ExactOnline.OpenApiGenerator;

var cts = new CancellationTokenSource();

AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
    cts.Cancel();
};

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

AppDomain.CurrentDomain.UnhandledException += (_, _) =>
{
    cts.Cancel();
};

Console.ForegroundColor = ConsoleColor.White;

var service = new OpenApiBuilderService();
return await service.InvokeAsync(args, cts.Token);