using System;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Actions
{
    /// <summary>Добавление описания действию</summary>
    /// <code>
    /// [ControllerDescription("Controller Description")]
    /// public class DescriptionAttributesController : Controller
    /// {
    ///     public string Index() => "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
    ///
    ///     [ActionDescription("Action Description")]
    ///     public string UseActionDescriptionAttribute() => "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
    /// }
    /// </code>
    public sealed class ActionDescriptionAttribute : Attribute, IActionModelConvention
    {
        /// <summary>Добавляемое описание</summary>
        private readonly string _Description;

        /// <summary>Инициализация нового экземпляра <see cref="ActionDescriptionAttribute"/></summary>
        /// <param name="Description">Добавляемое описание</param>
        public ActionDescriptionAttribute(string Description) => _Description = Description;

        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" /></summary>
        /// <param name="action">Модель действия контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" /></param>
        public void Apply([NotNull] ActionModel action) => action.Properties["description"] = _Description;
    }
}