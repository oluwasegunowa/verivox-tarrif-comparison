using System.Collections.Generic;
using System.Threading.Tasks;
using VerivoxTask.Application.DTO;

namespace VerivoxTask.Domain.Interfaces
{
    public interface ITarrifCalculationService
    {
       Task<List<ProductDTO>> CompareTarrif(int Consumption);
        Task<List<ProductDTO>> GetProducts();
    }
}
