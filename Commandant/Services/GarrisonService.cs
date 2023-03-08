using Grpc.Core;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Commandant.Services;

public class GarrisonService : Garrison.GarrisonBase
{
    public static string? InputDir = ConfigurationManager.AppSettings.Get("InputDir");
    private readonly ILogger<GarrisonService> _logger;
    
    public GarrisonService(ILogger<GarrisonService> logger)
    {
        _logger = logger;
    }

    public override Task<TaskResponse> SendTask(TaskRequest request, ServerCallContext context)
    {
        var task = TaskExecutor.GetTask(_logger);

        _logger.LogInformation("Отправка файла клиенту");
        
        return Task.FromResult(task);
    }
}