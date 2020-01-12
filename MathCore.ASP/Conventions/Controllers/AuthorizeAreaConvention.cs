using System;
using System.Linq;
using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Controllers
{
    /// <summary>Соглашение, добавляющее требование авторизации к указанной области</summary>
    /// <code>services.AddMvc(opt =&gt; options.Conventions.Add(new AuthorizeAreaConvention("Admin", "AdministratorPolicy")));</code>
    public class AuthorizeAreaConvention : IControllerModelConvention
    {
        /// <summary>Область, доступ к которой требует обязательной авторизации</summary>
        private readonly string _Area;

        /// <summary>Имя политики авторизации, которая налагается на указанную область</summary>
        private readonly string _Policy;

        /// <summary>Инициализация нового соглашения об авторизации для указанной области</summary>
        /// <param name="Area">Область, доступ к которой требует обязательной авторизации</param>
        /// <param name="Policy">Имя политики авторизации, которая налагается на указанную область</param>
        public AuthorizeAreaConvention(string Area, string Policy)
        {
            _Area = Area;
            _Policy = Policy;
        }

        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></summary>
        /// <param name="controller">Модель контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></param>
        public void Apply([NotNull] ControllerModel controller)
        {
            const StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            if (controller.Attributes.OfType<AreaAttribute>().Any(area => area.RouteValue.Equals(_Area, comparison))
                || controller.RouteValues.Any(r => r.Key.Equals("area", comparison) && r.Value.Equals(_Area, comparison)))
                controller.Filters.Add(new AuthorizeFilter(_Policy));
        }
    }
}