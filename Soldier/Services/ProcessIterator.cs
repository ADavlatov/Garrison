using System.Diagnostics;

namespace Soldier.Services;

public class ProcessIterator
{
    static Garrison.GarrisonClient? _client;
    static TaskResponse? _response;

    public ProcessIterator(Garrison.GarrisonClient? client)
    {
        Console.WriteLine("Старт");
        
        _client = client;
        _response = _client!.SendTask(new TaskRequest());
        
        Console.WriteLine("Содержимое файла: " + _response.File.ToStringUtf8());
        
        StartProcess(_response.File.ToStringUtf8(), _response.App);
    }

    private static void StartProcess(string args, string appDir)
    {
        Console.WriteLine("Запуск программы");
        
        var process = new Process();
        process.StartInfo.FileName = appDir;
        process.StartInfo.Arguments = args;
        process.EnableRaisingEvents = true;
        process.Exited += ProcessExited;
        process.Start();
    }

    private static async void ProcessExited(object? sender, EventArgs e)
    {
        Console.WriteLine("Завершение работы программы");
        Console.WriteLine("Запрос ответа от сервера");
        
        _response = await _client!.SendTaskAsync(new TaskRequest());

        if (!_response.File.IsEmpty)
        {
            StartProcess(_response.File.ToStringUtf8(), _response.App);
        }
    }
}