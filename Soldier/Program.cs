using System.Configuration;
using Grpc.Net.Client;
using Soldier;
using Soldier.Services;

var channel = GrpcChannel.ForAddress(ConfigurationManager.AppSettings.Get("HostAddress")!);
var client = new Garrison.GarrisonClient(channel);

new ProcessIterator(client);

Console.ReadKey();
