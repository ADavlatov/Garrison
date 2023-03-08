using Google.Protobuf;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Commandant.Services;

public static class TaskExecutor
{
    private static Queue<string> _filesList =
        new(Directory.GetFiles(GarrisonService.InputDir!));

    public static TaskResponse GetTask(ILogger logger)
    {
        if (_filesList.Count == 0)
        {
            logger.LogInformation("Очередь пуста");
            return new TaskResponse();
        }
        
        logger.LogInformation("Получение файла из директории");
        
        var file = _filesList.Dequeue();
        StreamReader sr = new StreamReader(file);

        return new TaskResponse
        {
            File = ByteString.FromStream(sr.BaseStream), App = ConfigurationManager.AppSettings.Get("AppDir")
        };
    }
}