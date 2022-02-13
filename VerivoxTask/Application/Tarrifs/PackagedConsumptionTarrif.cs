using System;
using VerivoxTask.Application.DTO;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Domain.Model;

namespace VerivoxTask.Application.Services
{
    public class PackagedConsumptionTarrif : ITarrifModel
    {
        public string TarrifName => "Packaged tariff";

        public ProductDTO CalculateTarrif(int Consumption)
        {
            if (Consumption <= 0)
            {
                throw new VerivoxException("You have supplied an invalid consumption.");
            }


            int baseConsumption = 4000;
            decimal baseFee = 800;
            decimal perKwCost = 0.30m;
            decimal annualCost = 0;

            //calculate the annual cost;
            if (Consumption <= baseConsumption) 
                annualCost = baseFee;
            else
                annualCost = (baseFee + (Consumption - baseConsumption) * perKwCost);

            return new ProductDTO() { TarrifName = TarrifName, AnnualCost = annualCost };
        }

    }
}
