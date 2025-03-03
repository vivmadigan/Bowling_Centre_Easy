using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Bowling_Centre_Easy.Logger
{
    /// <summary>
    /// A singleton logger that uses Microsoft.Extensions.Logging under the hood
    /// to send messages to the console (and potentially other providers).
    /// </summary>
    public sealed class SingletonLogger
    {
        // Step 1: Create the single, private static instance 
        private static readonly SingletonLogger _instance = new SingletonLogger();

        // Step 2: Store an ILogger (from Microsoft.Extensions.Logging)
        // We'll create a logger for this "SingletonLogger" category by default.
        private readonly ILogger _logger;

        // Step 3: Private constructor to prevent external instantiation.
        // Here, we configure Microsoft.Extensions.Logging (Console logging).
        private SingletonLogger()
        {
            // Build a logger factory that can create loggers with specific providers
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                // Add the console provider
                builder.AddConsole();

                // You can set log levels, add other providers, etc.
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            // Create a logger instance. The "SingletonLogger" string is the category.
            _logger = loggerFactory.CreateLogger("SingletonLogger");

            // Optionally: you might also store the factory if you plan to create more loggers.
        }

        // Step 4: Public accessor for the single instance
        public static SingletonLogger Instance => _instance;

        // Step 5: Expose log methods for different log levels (Info, Warning, Error, etc.)
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, Exception ex = null)
        {
            if (ex == null)
                _logger.LogError(message);
            else
                _logger.LogError(ex, message);
        }

        // Optionally, you can add LogDebug, LogCritical, etc. as well
    }
}
