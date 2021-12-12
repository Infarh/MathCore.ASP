using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using MathCore.ASP.Annotations;

using Microsoft.AspNetCore.Mvc.Filters;

namespace MathCore.ASP.Filters.Results
{
    /// <summary>Добавление времени обработки запроса</summary>
    public class ProcessingTimeHeaderAttribute : Attribute, IAsyncResultFilter
    {
        /// <summary>Имя заголовка</summary>
        [NotNull]
        private string Name { get; } = "Processing-Time";

        /// <summary>Инициализация нового экземпляра <see cref="ProcessingTimeHeaderAttribute"/></summary>
        public ProcessingTimeHeaderAttribute() { }

        /// <summary>Инициализация нового экземпляра <see cref="ProcessingTimeHeaderAttribute"/></summary>
        /// <param name="Name">Имя заголовка</param>
        public ProcessingTimeHeaderAttribute([NotNull] string Name) => 
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));

        /// <inheritdoc />
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var timer = Stopwatch.StartNew();

            context.HttpContext.Response.OnStarting(() =>
            {
                context.HttpContext.Response.Headers.Add(Name, $"{timer.Elapsed.TotalMilliseconds:0.##} ms");
                return Task.CompletedTask;
            });

            await next();
        }
    }
}
