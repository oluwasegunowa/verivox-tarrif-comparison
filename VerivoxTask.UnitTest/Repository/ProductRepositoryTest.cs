using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using VerivoxTask.Domain.Entities;
using VerivoxTask.Infrastructure.Persistence;
using VerivoxTask.Infrastructure.Persistence.Repository;
using Xunit;

namespace VerivoxTask.UnitTest
{
    public class ProductRepositoryTest
    {
        public Mock<ApplicationDbContext> _context;
        public Mock<DbSet<Product>> _dbSetMock;

        public ProductRepositoryTest()
        {

            _context = new Mock<ApplicationDbContext>();
            _dbSetMock = new Mock<DbSet<Product>>();
        }



        [Fact]
        public void Get_ProductById_ReturnProduct()
        {
            // Arrange


            _context.Setup(x => x.Set<Product>()).Returns(_dbSetMock.Object);
            _dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(new Product());

            // Act
            var repository = new Repository<Product>(_context.Object);
            var result = repository.Get(1);

            // Assert
            _context.Verify(x => x.Set<Product>());
            _dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_Products_ReturnsAll()
        {
            // Arrange

            var dbSetMock = CreateDbSetMock(MockDataSeed. GetProducts());

            var context = new Mock<ApplicationDbContext>();
            context.Setup(x => x.Set<Product>()).Returns(dbSetMock.Object);
          //  _dbSetMock.Setup(x => x.ToList()).Returns( MockDataSeed.GetProducts().ToList());

            // Act
            var repository = new Repository<Product>(context.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(6, result.Count());
        }


        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());
            return dbSetMock;
        }

     

    }
}
