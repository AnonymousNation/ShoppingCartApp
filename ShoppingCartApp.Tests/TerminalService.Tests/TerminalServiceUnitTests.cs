using Moq;
using ShoppingCartApp.Services;

namespace ShoppingCartApp.Tests
{
    public class TerminalServiceUnitTests
    {
        private ITerminal _sut;
        private Mock<IProductService> _productServiceMock;

        public TerminalServiceUnitTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _sut = new TerminalService(_productServiceMock.Object);
        }

        [Fact]
        public void TestProductA_ShouldCalculateNoBulkDiscount()
        {
            //Arrange
            Model.Product testProduct = new Model.Product()
            {
                 Code = 'A',
                 DiscountQuantity= 1,
                 DiscountQuantityPrice= 1,
                 Price = 2.0M
             };

            _productServiceMock.Setup(x => x.LookupProduct(It.IsAny<char>())).Returns(testProduct);

            //Act
            _sut.Scan("A"); //Could also scan "AA"
            _sut.Scan("A");
            var total = _sut.Total();

            //Assert
            Assert.Equal(4.0M, total);
            _productServiceMock.Verify(x => x.LookupProduct(It.IsAny<char>()), Times.Exactly(2));
        }

        [Fact]
        public void TestProductA_ShouldCalculateBulkDiscount()
        {
            //Arrange
            Model.Product testProduct = new Model.Product()
            {
                Code = 'A',
                DiscountQuantity = 4,
                DiscountQuantityPrice = 7.0M,
                Price = 2.0M
            };

            _productServiceMock.Setup(x => x.LookupProduct(It.IsAny<char>())).Returns(testProduct);

            //Act
            _sut.Scan("A");  
            _sut.Scan("A");
            _sut.Scan("A");
            _sut.Scan("A");
            var total = _sut.Total();

            //Assert
            Assert.Equal(7.0M, total);
            _productServiceMock.Verify(x => x.LookupProduct(It.IsAny<char>()), Times.Exactly(4));
        }

        [Fact]
        public void Test_CartEmpty()
        {
            //Arrange
            Model.Product testProduct = new Model.Product()
            {
                Code = 'A',
                DiscountQuantity = 4,
                DiscountQuantityPrice = 7.0M,
                Price = 2.0M
            };

            _productServiceMock.Setup(x => x.LookupProduct(It.IsAny<char>())).Returns(testProduct);

            //Act
            var total = _sut.Total();

            //Assert
            Assert.Equal(0.0M, total);
            _productServiceMock.Verify(x => x.LookupProduct(It.IsAny<char>()), Times.Never);
        }

        [Fact]
        public void TestMixedProduct_ShouldReturnCorrectAmount()
        {
            //Arrange
            Model.Product testProductA = new Model.Product()
            {
                Code = 'A',
                DiscountQuantity = 4,
                DiscountQuantityPrice = 7.0M,
                Price = 2.0M
            };

            Model.Product testProductB = new Model.Product()
            {
                Code = 'B',
                Price = 12.0M
            };

            Model.Product testProductC = new Model.Product()
            {
                Code = 'C',
                DiscountQuantity = 6,
                DiscountQuantityPrice = 6.0M,
                Price = 1.25M
            };

            Model.Product testProductD = new Model.Product()
            {
                Code = 'D',
                Price = 0.15M
            };

            _productServiceMock.Setup(x => x.LookupProduct('A')).Returns(testProductA);
            _productServiceMock.Setup(x => x.LookupProduct('B')).Returns(testProductB);
            _productServiceMock.Setup(x => x.LookupProduct('C')).Returns(testProductC);
            _productServiceMock.Setup(x => x.LookupProduct('D')).Returns(testProductD);

            //Act
            _sut.Scan("ABCD");
            var total = _sut.Total();

            //Assert
            Assert.Equal(15.4M, total);
            _productServiceMock.Verify(x => x.LookupProduct(It.IsAny<char>()), Times.Exactly(4));
        }

        [Fact]
        public void TestAddingProducts_ThenClear_ShouldHaveZeroDollarAmount()
        {
            //Arrange
            Model.Product testProductA = new Model.Product()
            {
                Code = 'A',
                DiscountQuantity = 4,
                DiscountQuantityPrice = 7.0M,
                Price = 2.0M
            };

            Model.Product testProductD = new Model.Product()
            {
                Code = 'D',
                Price = 0.15M
            };

            _productServiceMock.Setup(x => x.LookupProduct('A')).Returns(testProductA);
            _productServiceMock.Setup(x => x.LookupProduct('D')).Returns(testProductD);

            //Act
            _sut.Scan("AD");
            _sut.Clear();
            var total = _sut.Total();

            //Assert
            Assert.Equal(0.0M, total);
            _productServiceMock.Verify(x => x.LookupProduct(It.IsAny<char>()), Times.Exactly(2));
        }
    }
}