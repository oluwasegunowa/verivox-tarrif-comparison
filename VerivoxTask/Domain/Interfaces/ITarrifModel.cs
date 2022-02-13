using VerivoxTask.Application.DTO;

namespace VerivoxTask.Domain.Interfaces
{
    public interface ITarrifModel
    {

        ProductDTO CalculateTarrif(int Consumption);
        string TarrifName { get; }
    }
}
