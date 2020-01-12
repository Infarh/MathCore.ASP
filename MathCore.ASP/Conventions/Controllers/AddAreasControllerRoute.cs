using System;
using System.Linq;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Controllers
{
    /// <summary>Соглашение, добавляющее к контроллеру маршрут с указанием его области исходя из имени пространства имён его класса</summary>
    /// <code>services.AddMvc(opt =&gt; options.Conventions.Add(new AddAreasControllerRoute()));</code>
    public class AddAreasControllerRoute : IControllerModelConvention
    {
        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></summary>
        /// <param name="controller">Модель контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></param>
        public void Apply([NotNull] ControllerModel controller)
        {
            var type_namespace = controller.ControllerType.Namespace;
            if (string.IsNullOrEmpty(type_namespace)) return;

            const string areas_namespace_suffix = "Areas.";
            const int areas_namespace_suffix_length = 6;
            var areas_index = type_namespace.IndexOf(areas_namespace_suffix, StringComparison.OrdinalIgnoreCase);
            if (areas_index < 0) return;

            areas_index += areas_namespace_suffix_length;

            var area_name = type_namespace.Substring(areas_index, type_namespace.IndexOf('.', areas_index) - areas_index);
            if (string.IsNullOrEmpty(area_name)) return;

            if (controller.Attributes.OfType<AreaAttribute>().Any(a => a.RouteKey == "area" && a.RouteValue == area_name)) return;

            controller.RouteValues["area"] = area_name;
        }
    }
}