using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using Domain.Interfaces;
using Application.Common.Interfaces;

namespace Application.Modules.Queries
{
    public class GetRecommendedModulesQueryHandler : IRequestHandler<GetRecommendedModulesQuery, GetModulesResult>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IPredictionService _predictionService;

        public GetRecommendedModulesQueryHandler(IModuleRepository moduleRepository, IPredictionService predictionService)
        {
            _moduleRepository = moduleRepository;
            _predictionService = predictionService;
        }

        public async Task<GetModulesResult> Handle(GetRecommendedModulesQuery request, CancellationToken cancellationToken)
        {
            var recommendedModules = await _predictionService.GetRecommendedModulesAsync(request.DescriptionRequest);
            Console.WriteLine("Recommended modules: " + string.Join(", ", recommendedModules));
            var modules = _moduleRepository.GetModulesByNames(recommendedModules.ToArray());
            return new GetModulesResult(modules);
        }
    }
}
