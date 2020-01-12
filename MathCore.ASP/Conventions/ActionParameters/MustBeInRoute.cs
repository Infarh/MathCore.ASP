using System;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.ActionParameters
{
    /// <summary>Параметр обязан присутствовать в маршруте</summary>
    /// <code>
    /// public class ParameterModelController : Controller
    /// {
    ///     // Будет привязан:  /ParameterModel/GetById/123
    ///     // НЕ БУДЕТ привязан!: /ParameterModel/GetById?id=123
    ///     public string GetById([MustBeInRoute]int id) => $"Привязка к id: {id}";
    /// }
    /// </code>
    public sealed class MustBeInRoute : Attribute, IParameterModelConvention
    {
        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ParameterModel" /></summary>
        /// <param name="model">Модель параметра действия контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ParameterModel" /></param>
        public void Apply([NotNull] ParameterModel model)
        {
            if (model.BindingInfo is null) model.BindingInfo = new BindingInfo();
            model.BindingInfo.BindingSource = BindingSource.Path;
        }
    }
}