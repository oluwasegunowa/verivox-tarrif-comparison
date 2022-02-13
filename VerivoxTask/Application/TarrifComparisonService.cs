using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;
using VerivoxTask.Domain.Entities;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Infrastructure.Persistence.Repository;

namespace VerivoxTask.Application.Services
{
    public class TarrifComparisonService: ITarrifCalculationService
    {
        private readonly IEnumerable<ITarrifModel> _tarrifModels;
        private readonly IUnitOfWork _unitofWork;
        public TarrifComparisonService(IEnumerable<ITarrifModel> tarrifModels, IUnitOfWork unitofWork)
        {
            _tarrifModels = tarrifModels;
             _unitofWork=unitofWork;
        }

        public async Task<List<ProductDTO>> CompareTarrif(int Consumption)
        {

            List<ProductDTO> result = new List<ProductDTO>();

            foreach (var tarrifModel in _tarrifModels)
            {
                result.Add(tarrifModel.CalculateTarrif(Consumption));
            }

            //Order the list in ascending order of annual cost
            var orderedList = result.OrderBy(ord => ord.AnnualCost).ToList(); 


            //Add the result into the Product table only if there is no calculation for the consumption value
            if (!(await _unitofWork.ProductRepository.Find(c=>c.Consumption==Consumption)).Any())
            {
                _unitofWork.ProductRepository.AddList(orderedList.Select(s => new Product() { AnnualCost = s.AnnualCost, Consumption = Consumption, TarrifName = s.TarrifName }));
                await _unitofWork.Complete();
            }
            
            return orderedList;

        }

        public async Task<List<ProductDTO>> GetProducts()
        {
           
            var result = _unitofWork.ProductRepository.GetAll().OrderBy(ord => ord.Consumption).ThenBy(ord => ord.AnnualCost).Select(s => new ProductDTO() { AnnualCost=s.AnnualCost, TarrifName=s.TarrifName  }).ToList();

            return result;
        }
    }
}
