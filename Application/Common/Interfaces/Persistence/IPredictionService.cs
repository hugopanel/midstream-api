using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPredictionService
    {
        Task<List<string>> GetRecommendedModulesAsync(string description);
    }
}
