
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Modules.Queries;

public class GetAllModulesQueryHandler(IModuleRepository moduleRepository)
    :IRequestHandler<GetAllModulesQuery, GetModulesResult>
{
    private IModuleRepository _moduleRepository = moduleRepository;

    public async Task<GetModulesResult> Handle(GetAllModulesQuery request, CancellationToken cancellationToken)
    {
        var modules = _moduleRepository.GetAllModules();        
        return new GetModulesResult(modules);
    }

}
