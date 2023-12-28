using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SpsAssessment.Helpers;
using SpsAssessment.Helpers.Abstractions;
using SPSAssessment.Domain;
using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Models;
using SPSAssessment.OrderProcessor;
using System.Security.Authentication.ExtendedProtection;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Order processing");

        var sericeCollection = new ServiceCollection();

         
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddTransient<IFixedLengthContentDeserializer, FixedLengthContentDeserializer>()
            .AddTransient<IXMLSerializer, XMLSerializer>()
            .AddTransient<IRestService, RestService>()
            .AddTransient<INotificationService, NotificationService>()
            .AddTransient<IInventoryService, InventoryService>()
            .AddTransient<IArticlePriceService, ArticlePriceService>()
            .AddTransient<IArticleService, ArticleService>()
            .AddTransient<IOrderManagementService, OrderManagementService>()
            .AddTransient<IOrderService, OrderService>()
            .AddTransient<IFixedLengthFileProcessor, OrderProcessor>()

            .BuildServiceProvider();


         
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();         
        Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();
       
        Console.WriteLine($"Order directory Path -{settings.OrderPath}");

        var orderFiles = Directory.GetFiles(settings.OrderPath); 

        var orderProcessor = serviceProvider.GetService<IFixedLengthFileProcessor>();
        foreach (var item in orderFiles)
        {
            Console.WriteLine($"Started Processing file  - {item}");

            var processFileTask = orderProcessor.ProcessFileAsyc(orderFiles.FirstOrDefault());

            processFileTask.Wait();

            Console.WriteLine($"Message from Process File operation - {processFileTask.Result}");
        } 
    } 
}