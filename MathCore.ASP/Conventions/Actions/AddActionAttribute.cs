using System;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Conventions.Actions
{
    /// <summary>Добавление псевдонима действия контроллера</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AddActionAttribute : Attribute, IActionModelConvention
    {
        /// <summary>Имя псевдонима действия</summary>
        private readonly string _Name;

        /// <summary>Инициализация нового экземпляра <see cref="AddActionAttribute"/></summary>
        /// <param name="Name">Имя псевдонима действия</param>
        public AddActionAttribute(string Name) => _Name = Name;

        /// <summary>Вызывается для применения соглашения к <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" /></summary>
        /// <param name="action">Модель действия контроллера <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" /></param>
        public void Apply([NotNull] ActionModel action) => action.Controller.Actions.Add(new ActionModel(action) { ActionName = _Name });
    }
}