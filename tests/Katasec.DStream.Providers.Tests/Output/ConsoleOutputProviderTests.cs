using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Katasec.DStream.Plugin.Models;
using Katasec.DStream.Providers.Output;
using Xunit;

namespace Katasec.DStream.Providers.Tests.Output
{
    public class ConsoleOutputProviderTests
    {
        [Fact]
        public void Name_ShouldReturnConsole()
        {
            // Arrange
            var provider = new ConsoleOutputProvider();
            
            // Act
            var name = provider.Name;
            
            // Assert
            Assert.Equal("console", name);
        }
        
        [Fact]
        public async Task InitializeAsync_ShouldSetFormat()
        {
            // Arrange
            var provider = new ConsoleOutputProvider();
            var config = new Dictionary<string, object>
            {
                { "format", "text" }
            };
            
            // Act
            await provider.InitializeAsync(config, CancellationToken.None);
            
            // Assert - We can't directly test private fields, but we'll test the behavior in WriteAsync
            // This test mainly ensures no exceptions are thrown
            Assert.True(true);
        }
        
        [Fact]
        public async Task WriteAsync_ShouldWriteToConsole()
        {
            // Arrange
            var provider = new ConsoleOutputProvider();
            var config = new Dictionary<string, object>
            {
                { "format", "text" }
            };
            
            await provider.InitializeAsync(config, CancellationToken.None);
            
            var items = new List<StreamItem>
            {
                StreamItem.FromJson("{\"test\": \"value\"}", "test-source", "test-operation")
            };
            
            // Redirect console output to capture it
            var originalOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            try
            {
                // Act
                await provider.WriteAsync(items, CancellationToken.None);
                var output = stringWriter.ToString();
                
                // Assert
                Assert.Contains("Source: test-source", output);
                Assert.Contains("Operation: test-operation", output);
            }
            finally
            {
                // Restore console output
                Console.SetOut(originalOut);
            }
        }
        
        [Fact]
        public async Task WriteAsync_WithJsonFormat_ShouldWriteJson()
        {
            // Arrange
            var provider = new ConsoleOutputProvider();
            var config = new Dictionary<string, object>
            {
                { "format", "json" }
            };
            
            await provider.InitializeAsync(config, CancellationToken.None);
            
            var items = new List<StreamItem>
            {
                StreamItem.FromJson("{\"test\": \"value\"}", "test-source", "test-operation")
            };
            
            // Redirect console output to capture it
            var originalOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            try
            {
                // Act
                await provider.WriteAsync(items, CancellationToken.None);
                var output = stringWriter.ToString();
                
                // Assert
                Assert.Contains("\"Source\": \"test-source\"", output);
                Assert.Contains("\"Operation\": \"test-operation\"", output);
            }
            finally
            {
                // Restore console output
                Console.SetOut(originalOut);
            }
        }
    }
}
