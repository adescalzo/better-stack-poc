# better-stack-poc

Better Stack logging PoC

## Simple doc

Package

```shell
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Extensions.Logging
dotnet add package BetterStack.Logs.Serilog
```

appsettings.json

```json
  "Serilog": {
    "Using": ["Serilog.Sinks.Console", "BetterStack.Logs.Serilog"],
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "BetterStack",
        "Args": {
          "sourceToken": "[SourceToken]",
          "queueLimitBytes": 10485760,
          "batchSize": 100,
          "encoding": "System.Text.Encoding::UTF8",
          "restrictedToMinimumLevel": "Information"
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName"],
    "Properties": {
      "ApplicationName": "BetterStack PoC App"
    }
  }
```

Set your "sourceToken" in the configuration
