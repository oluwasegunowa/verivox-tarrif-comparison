using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;
using VerivoxTask.Controllers;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Domain.Model;
using Xunit;

namespace VerivoxTask.UnitTest
{
    public class TarrifControllerTest
    {

        Mock<ITarrifCalculationService> _tarrifServiceMock;
        int consumption = 3500;
        public TarrifControllerTest()
        {
             _tarrifServiceMock = new Mock<ITarrifCalculationService>();
           
            _tarrifServiceMock.Setup(m => m.CompareTarrif(It.Is<int>(a => a <= 0))).Throws(new VerivoxException("You have supplied an invalid consumption.") { }); //(MockDataSeed.GetProducts(consumption));
            _tarrifServiceMock.Setup(m => m.CompareTarrif(It.Is<int>(a => a > 0))).Returns(Task.FromResult(MockDataSeed.GetProducts(consumption).ToList()));


        }
        [Fact]
        public async Task CompareTarrif_With0OrLess_ShouldReturnBadRequest()
        {
            //Arrange

            var logger = Mock.Of<ILogger<TarrifController>>();
            var controller = new TarrifController(logger, _tarrifServiceMock.Object);


            //Act
            var Exception = await Assert.ThrowsAsync<VerivoxException>(async () => await controller.CompareTarrif(new ComparisonRequestDTO() { Consumption = 0 }));
        
            
            Assert.NotEmpty(Exception.Message);
        }



        [Fact]
        public async Task CompareTarrif_WithMoreThan0_ShouldReturn2Products()
        {
            //Arrange
        
            var logger = Mock.Of<ILogger<TarrifController>>();
            var controller = new TarrifController(logger, _tarrifServiceMock.Object);

            //Act
            var result = await controller.CompareTarrif(new ComparisonRequestDTO() { Consumption = consumption });

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
           
            
            Assert.IsType<List<ProductDTO>>(okObjectResult.Value);

           Assert.Equal(2,((List<ProductDTO>) okObjectResult.Value).Count);
        }



    }
}
