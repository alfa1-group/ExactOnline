using ExactOnline.OpenApiGenerator;

var cts = new CancellationTokenSource();

AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    cts.Cancel();
};

Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    cts.Cancel();
};

Console.ForegroundColor = ConsoleColor.White;

var service = new OpenApiBuilderService();
return await service.InvokeAsync(args, cts.Token);