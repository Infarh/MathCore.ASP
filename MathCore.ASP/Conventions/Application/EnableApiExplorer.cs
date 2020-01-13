using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Application
{
    /// <summary>Включение отображения информации по WebAPI, представляемого приложением</summary>
    /// <code>services.AddMvc(opt =&gt; options.Conventions.Add(new EnableApiExplorer()));</code>
    public class EnableApiExplorer : IApplicationModelConvention
    {
        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></summary>
        /// <param name="application">Модель приложения <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModel" /></param>
        public void Apply([NotNull] ApplicationModel application) => application.ApiExplorer.IsVisible = true;
    }
}
