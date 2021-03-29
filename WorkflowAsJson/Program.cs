using Elsa.Activities.Console.Extensions;
using Elsa.Models;
using Elsa.Serialization;
using Elsa.Serialization.Formatters;
using Elsa.Services;
using Elsa.Test.Activities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Elsa.Test
{
    class Program
    {
        private static async Task Main()
        {
            var json = await ReadEmbeddedResourceAsync("elsaworkflowasjson.workflow.json");
            var services = BuildServices();
            var serializer = services.GetRequiredService<IWorkflowSerializer>();
            var workflow = serializer.Deserialize<WorkflowDefinitionVersion>(json, JsonTokenFormatter.FormatName);
            var invoker = services.GetRequiredService<IWorkflowInvoker>();

            await invoker.StartAsync(workflow);
        }

        private static IServiceProvider BuildServices()
        {
            return new ServiceCollection()
                .AddElsa()
                .AddConsoleActivities()
                .AddActivity<CustomActivityOne>()
                .BuildServiceProvider();
        }

        private static async Task<string> ReadEmbeddedResourceAsync(string resourceName)
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            using var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName));
            return await reader.ReadToEndAsync();
        }
    }
}