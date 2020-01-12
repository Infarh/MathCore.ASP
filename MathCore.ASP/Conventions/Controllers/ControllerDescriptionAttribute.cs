using System;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Controllers
{
    /// <summary>Описание контроллера</summary>
    /// <code>
    /// [ControllerDescription("Controller Description")]
    /// public class DescriptionAttributesController : Controller
    /// {
    ///     public string Index() => "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
    /// }
    /// </code>
    public sealed class ControllerDescriptionAttribute : Attribute, IControllerModelConvention
    {
        /// <summary>Добавляемое описание</summary>
        private readonly string? _Description;

        /// <summary>Инициализация нового экземпляра <see cref="ControllerDescriptionAttribute"/></summary>
        /// <param name="Description">Добавляемое описание</param>
        public ControllerDescriptionAttribute(string? Description) => _Description = Description;

        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></summary>
        /// <param name="controller">Модель контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerModel" /></param>
        public void Apply([NotNull] ControllerModel controller) => controller.Properties["description"] = _Description;
    }
}