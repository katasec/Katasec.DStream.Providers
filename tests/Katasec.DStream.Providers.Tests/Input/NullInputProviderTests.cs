using System.Threading;
using System.Threading.Tasks;
using Katasec.DStream.Providers.Input;
using Xunit;

namespace Katasec.DStream.Providers.Tests.Input
{
    public class NullInputProviderTests
    {
        [Fact]
        public void Name_ShouldReturnNull()
        {
            // Arrange
            var provider = new NullInputProvider();
            
            // Act
            var name = provider.Name;
            
            // Assert
            Assert.Equal("null", name);
        }
        
        [Fact]
        public async Task ReadAsync_ShouldReturnEmptyCollection()
        {
            // Arrange
            var provider = new NullInputProvider();
            
            // Act
            var result = await provider.ReadAsync(CancellationToken.None);
            
            // Assert
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task HasMoreDataAsync_ShouldReturnFalse()
        {
            // Arrange
            var provider = new NullInputProvider();
            
            // Act
            var result = await provider.HasMoreDataAsync(CancellationToken.None);
            
            // Assert
            Assert.False(result);
        }
    }
}
