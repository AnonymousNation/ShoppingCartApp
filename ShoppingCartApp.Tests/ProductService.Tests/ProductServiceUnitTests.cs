using Moq;
using ShoppingCartApp.Services;

namespace ShoppingCartApp.Tests
{
    public class ProdcutServiceUnitTests
    {
        private IProductService _sut;
        public ProdcutServiceUnitTests()
        {
            _sut = new ProductService();
        }

        [Fact]
        public void TestProductA_FoundAtExpectedPrice()
        {
            //Arrange

            //Act
            var product = _sut.LookupProduct('A');

            //Assert
            Assert.NotNull(product);
            Assert.Equal(2.0M, product.Price);
        }

        [Fact]
        public void TestProductX_NotFoundException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.LookupProduct('X'));
        }
    }
}