using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api;

public class ModuleRouteConvention : IApplicationModelConvention
{
    private readonly string _modulePrefix;
    private readonly string _moduleNamespace;

    public ModuleRouteConvention(string modulePrefix, string moduleNamespace)
    {
        _modulePrefix = modulePrefix;
        _moduleNamespace = moduleNamespace;
    }
    
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.Namespace == _moduleNamespace)
            {
                var attributeRouteModel = controller.Selectors[0].AttributeRouteModel;
                if (attributeRouteModel != null)
                    attributeRouteModel.Template =
                        _modulePrefix + "/" + attributeRouteModel.Template;
            }
        }
    }
}