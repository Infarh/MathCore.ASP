using System;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Filters.Results
{
    /// <summary>Добавление заголовка к ответу</summary>
    public sealed class AddHeaderAttribute : ResultFilterAttribute
    {
        /// <summary>Имя заголовка</summary>
        [NotNull]
        private readonly string _Name;

        /// <summary>Значение заголовка</summary>
        private readonly string _Value;

        /// <summary>Инициализация нового экземпляра <see cref="AddHeaderAttribute"/></summary>
        /// <param name="Name">Имя заголовка</param>
        /// <param name="Value">Значение заголовка</param>
        public AddHeaderAttribute([NotNull] string Name, string Value)
        {
            _Name = Name ?? throw new ArgumentNullException(nameof(Name));
            _Value = Value;
        }

        /// <inheritdoc />
        public override void OnResultExecuting([NotNull] ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_Name, new[] { _Value });
            base.OnResultExecuting(context);
        }
    }
}
