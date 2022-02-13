using System;
using VerivoxTask.Application.DTO;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Domain.Model;

namespace VerivoxTask.Application.Services
{
   
    public class BasicConsumptionTarrif : ITarrifModel
    {
        public string TarrifName => "basic electricity tariff";
        public ProductDTO CalculateTarrif(int Consumption)
        {
            if (Consumption <= 0)
            {
                throw new VerivoxException("You have supplied an invalid consumption.");
            }

            decimal basemonthlyFee = 5;
            decimal perKwCost =0.22m;//this is in cent

            return new ProductDTO() { TarrifName = TarrifName, AnnualCost = (basemonthlyFee * 12) + (Consumption * perKwCost) };
        }
    }
}
