using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Application
{
    /// <summary>Применение пространств имён в системе маршрутизации приложения</summary>
    /// <code>
    /// services.AddMvc(opt =&gt; options.Conventions.Add(new NamespaceRouting()));
    /// ...
    /// namespace AppModelSample.Controllers
    /// {
    ///     public class NamespaceRoutingController : Controller
    ///     {
    ///         // using NamespaceRouting
    ///         // route: /AppModelSample/Controllers/NamespaceRouting/Index
    ///         public string Index() => "This demonstrates namespace routing.";
    ///     }
    /// }
    /// </code>
    public class NamespaceRouting : IApplicationModelConvention
    {
        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></summary>
        /// <param name="application">Модель приложения <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var has_attribute_route = controller.Selectors.Any(s => s.AttributeRouteModel != null);

                if (!has_attribute_route)
                {
                    // Замена всех . в пространстве имён на/ для создания атрибута маршрута
                    // На пример: пространство имён MySite.Admin  будет преобразовано в маршрут MySite/Admin
                    // После этого применяются [controller], [action] и опционально {id?}.
                    // [Controller] и [action] заменены на имя и действие контроллера
                    // для создания готового шаблона маршрута
                    var controller_type_namespace = controller.ControllerType.Namespace 
                        ?? throw new InvalidOperationException("Отсутствует ссылка на название пространства имён контроллера");
                    controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = $"{controller_type_namespace.Replace('.', '/')}/[controller]/[action]/{{id?}}"
                    };
                }
            }
        }
    }
}
