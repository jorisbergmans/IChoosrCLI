﻿using Logic.Implementations;
using Logic.Services;
using Logic.Services.Implementations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace CLISearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup for the DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ISearchHandler, SearchHandler>()
                .AddSingleton<ICSVHelper, CSVHelper>()
                .BuildServiceProvider();

            //Setup for the logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger("Debug");

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //Executing the logic
            var search = serviceProvider.GetService<ISearchHandler>();
            if (args.Length != 0)
            {
                var cameraResult = search.ShowCamerasByName(args[1]);
                cameraResult.ForEach(x => Console.Write("{0}|{1}|{2}\r\n", x.Camera, x.Latitude, x.Longitude));
            }

            logger.LogDebug("Finished succesfully!");
        }
    }
}
