# Katasec.DStream.Providers

Standard input and output providers for DStream plugins.

## Overview

This package contains reusable input and output providers for DStream plugins. It provides:

- Base classes for creating custom providers
- Standard implementations of common providers
- Provider registration and discovery mechanism

## Included Providers

### Input Providers

- **NullInputProvider**: A provider that doesn't produce any input data, useful for plugins that generate their own data.

### Output Providers

- **ConsoleOutputProvider**: A provider that writes output to the console in either JSON or text format.

## Usage

### Installation

```bash
dotnet add package Katasec.DStream.Providers
```

### Using Standard Providers

```csharp
using Katasec.DStream.Providers;
using Katasec.DStream.Plugin;

// Get a provider by name
var inputProvider = ProviderRegistry.GetInputProvider("null");
var outputProvider = ProviderRegistry.GetOutputProvider("console");

// Initialize with configuration
await inputProvider.InitializeAsync(new Dictionary<string, object>(), cancellationToken);
await outputProvider.InitializeAsync(new Dictionary<string, object> 
{
    { "format", "json" }
}, cancellationToken);

// Use in your plugin
await plugin.ProcessAsync(inputProvider, outputProvider, config, cancellationToken);
```

### Creating Custom Providers

```csharp
using Katasec.DStream.Providers.Input;
using Katasec.DStream.Plugin.Models;

public class MyCustomInputProvider : InputProviderBase
{
    public override string Name => "my-custom-input";

    public override Task<IEnumerable<StreamItem>> ReadAsync(CancellationToken cancellationToken)
    {
        // Custom implementation
    }

    public override Task<bool> HasMoreDataAsync(CancellationToken cancellationToken)
    {
        // Custom implementation
    }
}

// Register your custom provider
ProviderRegistry.RegisterInputProvider<MyCustomInputProvider>("my-custom-input");
```

## License

MIT
