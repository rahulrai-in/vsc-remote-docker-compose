using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace Ping
{
    class Program
    {
        private static string pongServiceUri = Environment.GetEnvironmentVariable("PONG_ADDRESS");
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Write("Ping");
                var cts = new CancellationTokenSource();
                var progressTask = Task.Run(() => ShowProgress(cts.Token), cts.Token);
                var pongResponse = await client.GetStringAsync(pongServiceUri);
                cts.Cancel();
                Console.WriteLine(pongResponse);
                progressTask.Wait();
            }
        }
        private static void ShowProgress(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.Write(".");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
