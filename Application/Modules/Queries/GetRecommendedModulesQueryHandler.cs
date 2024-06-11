
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Queries;

public class GetRecommendedModulesQueryHandler(IModuleRepository moduleRepository)
    :IRequestHandler<GetRecommendedModulesQuery, GetModulesResult>
{
    private IModuleRepository _moduleRepository = moduleRepository;

    public async Task<GetModulesResult> Handle(GetRecommendedModulesQuery request, CancellationToken cancellationToken)
    {
        List<Module> allModules = _moduleRepository.GetAllModules();
        List<Module> modules = new List<Module>();
        if (request.descriptionRequest == null)
        {            
            throw new Exception("Description is required");
        }
        else
        {            
            foreach (var module in allModules)
            {
                if (module.Tags.Any(tag => request.descriptionRequest.Contains(tag)))
                {
                    modules.Add(module);
                }
            }
        }        
        return new GetModulesResult(modules);
    }

}
