using System;
using System.Configuration;
using System.Reflection;

namespace FootballStatisticsEngine.Connections
{
    public static class ConnectionStringResolver
    {
        public static string GetConnectionString(string name)
        {

            // Retrieve the raw connection string from app.config
            var rawConnectionString = ConfigurationManager.ConnectionStrings[name]?.ConnectionString;

            if (string.IsNullOrEmpty(rawConnectionString))
            {
                throw new ArgumentException($"Connection string '{name}' not found in app.config.");
            }

            // Check if the connection string starts with "env:"
            if (rawConnectionString.StartsWith("env:", StringComparison.OrdinalIgnoreCase))
            {
                // Extract the environment variable name
                string envVariableName = rawConnectionString.Substring(4); // Remove "env:" prefix
                string envValue = Environment.GetEnvironmentVariable(envVariableName);

                if (string.IsNullOrEmpty(envValue))
                {
                    throw new InvalidOperationException($"Environment variable '{envVariableName}' is not set or empty.");
                }

                return envValue;
            }

            // Return the raw connection string if no "env:" prefix is found
            return rawConnectionString;
        }
    }
}