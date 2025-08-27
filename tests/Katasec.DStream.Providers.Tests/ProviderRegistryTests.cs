using System;
using System.Linq;
using Katasec.DStream.Plugin.Interfaces;
using Katasec.DStream.Providers.Input;
using Katasec.DStream.Providers.Output;
using Xunit;

namespace Katasec.DStream.Providers.Tests
{
    public class ProviderRegistryTests
    {
        [Fact]
        public void GetInputProvider_ShouldReturnNullInputProvider()
        {
            // Act
            var provider = ProviderRegistry.GetInputProvider("null");
            
            // Assert
            Assert.NotNull(provider);
            Assert.IsType<NullInputProvider>(provider);
            Assert.Equal("null", provider.Name);
        }
        
        [Fact]
        public void GetOutputProvider_ShouldReturnConsoleOutputProvider()
        {
            // Act
            var provider = ProviderRegistry.GetOutputProvider("console");
            
            // Assert
            Assert.NotNull(provider);
            Assert.IsType<ConsoleOutputProvider>(provider);
            Assert.Equal("console", provider.Name);
        }
        
        [Fact]
        public void GetInputProvider_WithInvalidName_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => ProviderRegistry.GetInputProvider("invalid"));
        }
        
        [Fact]
        public void GetOutputProvider_WithInvalidName_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => ProviderRegistry.GetOutputProvider("invalid"));
        }
        
        [Fact]
        public void HasInputProvider_WithValidName_ShouldReturnTrue()
        {
            // Act
            var result = ProviderRegistry.HasInputProvider("null");
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void HasOutputProvider_WithValidName_ShouldReturnTrue()
        {
            // Act
            var result = ProviderRegistry.HasOutputProvider("console");
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void HasInputProvider_WithInvalidName_ShouldReturnFalse()
        {
            // Act
            var result = ProviderRegistry.HasInputProvider("invalid");
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void HasOutputProvider_WithInvalidName_ShouldReturnFalse()
        {
            // Act
            var result = ProviderRegistry.HasOutputProvider("invalid");
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void GetInputProviderNames_ShouldContainNullProvider()
        {
            // Act
            var names = ProviderRegistry.GetInputProviderNames();
            
            // Assert
            Assert.Contains("null", names);
        }
        
        [Fact]
        public void GetOutputProviderNames_ShouldContainConsoleProvider()
        {
            // Act
            var names = ProviderRegistry.GetOutputProviderNames();
            
            // Assert
            Assert.Contains("console", names);
        }
        
        [Fact]
        public void RegisterInputProvider_ShouldAddProviderToRegistry()
        {
            // Arrange
            var testProviderName = "test-input";
            
            // Act
            ProviderRegistry.RegisterInputProvider<NullInputProvider>(testProviderName);
            
            // Assert
            Assert.True(ProviderRegistry.HasInputProvider(testProviderName));
            var provider = ProviderRegistry.GetInputProvider(testProviderName);
            Assert.NotNull(provider);
            Assert.IsType<NullInputProvider>(provider);
        }
        
        [Fact]
        public void RegisterOutputProvider_ShouldAddProviderToRegistry()
        {
            // Arrange
            var testProviderName = "test-output";
            
            // Act
            ProviderRegistry.RegisterOutputProvider<ConsoleOutputProvider>(testProviderName);
            
            // Assert
            Assert.True(ProviderRegistry.HasOutputProvider(testProviderName));
            var provider = ProviderRegistry.GetOutputProvider(testProviderName);
            Assert.NotNull(provider);
            Assert.IsType<ConsoleOutputProvider>(provider);
        }
    }
}
