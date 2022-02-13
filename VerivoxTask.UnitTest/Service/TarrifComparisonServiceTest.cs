using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;
using VerivoxTask.Application.Services;
using VerivoxTask.Controllers;
using VerivoxTask.Domain.Entities;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Infrastructure.Persistence.Repository;
using Xunit;

namespace VerivoxTask.UnitTest
{
    public class TarrifComparisonServiceTest
    {

        private int Consumption = 4500;
        private List<ITarrifModel> ModelsMock;
        private Mock<IUnitOfWork> UnitOfWorkMock;
        public TarrifComparisonServiceTest()
        {
            var modelBasicMock = new Mock<ITarrifModel>();
            modelBasicMock.Setup(m => m.CalculateTarrif(It.IsAny<int>())).Returns(GetBasicProduct());
            var modelPackagedMock = new Mock<ITarrifModel>();
            modelPackagedMock.Setup(m => m.CalculateTarrif(It.IsAny<int>())).Returns(GetPackagedProduct());


            ModelsMock = new List<ITarrifModel>() { modelBasicMock.Object, modelPackagedMock.Object };
            UnitOfWorkMock = new Mock<IUnitOfWork>();

            var repositoryMock = new Mock<IRepository<Product>>();
            repositoryMock.Setup(s => s.Find(It.IsAny<Expression<Func<Product, bool>>>()))
          .ReturnsAsync( new List<Product>() { MockDataSeed.GetPackagedProducts().FirstOrDefault() });

            UnitOfWorkMock.Setup(m => m.ProductRepository).Returns(repositoryMock.Object);

        }


        [Fact]
        public async Task CompareTarrif_WithConsumtpion_ShouldReturn2OrderedProductsForEachPackage()
        {

            //Arrange
           
            var service = new TarrifComparisonService(ModelsMock, UnitOfWorkMock.Object);


            //Act
            var result = await service.CompareTarrif(Consumption);

            //Assert
     
            Assert.Equal(ModelsMock.Count, result.Count());
            Assert.True(result[0].AnnualCost<= result[1].AnnualCost, "Must be arranged in ascending order of annual cost");
        }





        public ProductDTO GetBasicProduct()
        {
            return MockDataSeed.GetBasicProducts().Where(s => s.Consumption == Consumption).Select(s => new ProductDTO()
            {
                AnnualCost = s.AnnualCost,
                TarrifName = s.TarrifName
            }).FirstOrDefault();
        }

        public ProductDTO GetPackagedProduct()
        {
            return MockDataSeed.GetPackagedProducts().Where(s => s.Consumption == Consumption).Select(s => new ProductDTO()
            {
                AnnualCost = s.AnnualCost,
                TarrifName = s.TarrifName
            }).FirstOrDefault();
        }
    }

}

