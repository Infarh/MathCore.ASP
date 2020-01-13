using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Application
{
    /// <summary>Добавление описания к приложению</summary>
    /// <code>
    /// services.AddMvc(opt =&gt; options.Conventions.Add(new ApplicationDescription("App description")));
    /// ...
    /// public class HomeController : Controller
    /// {
    ///     public string AppDescription() => "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
    /// }
    /// </code>
    public class ApplicationDescription : IApplicationModelConvention
    {
        /// <summary>Добавляемое описание</summary>
        private readonly string? _Description;

        /// <summary>Инициализация нового экземпляра <see cref="ApplicationDescription"/></summary>
        /// <param name="Description">Добавляемое описание</param>
        public ApplicationDescription(string? Description) => _Description = Description;

        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></summary>
        /// <param name="application">Модель приложения <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></param>
        public void Apply([NotNull] ApplicationModel application) => application.Properties["description"] = _Description;
    }
}