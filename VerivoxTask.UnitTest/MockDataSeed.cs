using System.Collections.Generic;
using System.Linq;
using VerivoxTask.Application.DTO;
using VerivoxTask.Domain.Entities;

namespace VerivoxTask.UnitTest
{
    public static class MockDataSeed
    {

        public static IEnumerable<ProductDTO> GetProducts(int consumption)
        {
            return GetProducts().Where(c => c.Consumption == consumption).Select(s=> new ProductDTO() { AnnualCost=s.AnnualCost, TarrifName=s.TarrifName }).ToList();
        }


        public static IEnumerable<Product> GetProducts()
        {
            var retList = new List<Product>();

             retList.AddRange(GetBasicProducts());
            retList.AddRange(GetPackagedProducts());
            return retList;
        }


        public static IEnumerable<Product> GetBasicProducts()
        {
            var retList = new List<Product>
        {
            new Product {Id = 1, Consumption=3500,   TarrifName = "basic electricity tariff",   AnnualCost =830 },
            new Product {Id = 2,  Consumption=4500,   TarrifName = "basic electricity tariff",   AnnualCost =1050},
            new Product {Id = 3, Consumption=6000,   TarrifName = "basic electricity tariff",   AnnualCost =1320 },



        };

            return retList;
        }

        public static IEnumerable<Product> GetPackagedProducts()
        {
            var retList = new List<Product>
        {

             new Product {Id = 1, Consumption=3500,   TarrifName = "Packaged tariff",   AnnualCost =800 },
            new Product {Id = 2,  Consumption=4500,   TarrifName = "Packaged tariff",   AnnualCost =950},
            new Product {Id = 3, Consumption=6000,   TarrifName = "Packaged tariff",   AnnualCost =1400 }
        };

            return retList;
        }

    }
}
