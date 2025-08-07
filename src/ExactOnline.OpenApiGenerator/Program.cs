using ExactOnline.OpenApiGenerator;

Console.ForegroundColor = ConsoleColor.White;

var service = new OpenApiBuilderService();
return await service.InvokeAsync(args);