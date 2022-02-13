using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;
using VerivoxTask.Application.Services;
using VerivoxTask.Controllers;
using VerivoxTask.Domain.Model;
using Xunit;

namespace VerivoxTask.UnitTest
{
    public class PackagedConsumptionTarrifTest
    {

        private int InvalidConsumption = -100;
        private int ValidConsumption = 3500;
        public PackagedConsumptionTarrifTest()
        {
          
        }


        [Fact]
        public async Task CalculateTarrif_ValidConsumption_ReturnAnnualCost()
        {

            //Arrange
            var service = new BasicConsumptionTarrif();
            var sampleProduct = GetBasicProduct();

            //Act
            var result =  service.CalculateTarrif(ValidConsumption);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(sampleProduct.AnnualCost, sampleProduct.AnnualCost);
        }


        [Fact]
        public async Task CalculateTarrif_InValidConsumption_ThrowException()
        {

            //Arrange
            var service = new BasicConsumptionTarrif();
            var sampleProduct = GetBasicProduct();

            //Act
         
            //Assert
            var Exception =  Assert.Throws<VerivoxException>(() => service.CalculateTarrif(InvalidConsumption));
            Assert.NotEmpty(Exception.Message);
            Assert.Equal("You have supplied an invalid consumption.",Exception.Message);
        }




        public ProductDTO GetBasicProduct()
        {
            return MockDataSeed.GetPackagedProducts().Where(s => s.Consumption == ValidConsumption).Select(s => new ProductDTO()
            {
                AnnualCost = s.AnnualCost,
                TarrifName = s.TarrifName
            }).FirstOrDefault();
        }

       
    }

}

