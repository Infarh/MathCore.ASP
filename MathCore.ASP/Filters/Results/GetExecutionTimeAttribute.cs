using System;
using System.Diagnostics;
using System.Globalization;
using MathCore.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Filters.Results
{
    /// <summary>Добавление заголовка со значением полного времени выполнения действия контроллера</summary>
    public class GetExecutionTimeAttribute : Attribute, IResultFilter
    {
        private readonly Stopwatch _Timer = new Stopwatch();

        /// <summary>Вызывается до начала выполнения действия контроллера</summary>
        /// <param name="context">Контекст запроса <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext" /></param>
        public void OnResultExecuting(ResultExecutingContext context) => _Timer.Start();

        /// <summary>Вызывается после получения результата действия</summary>
        /// <param name="context">Контекст запроса <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutedContext" /></param>
        public void OnResultExecuted([NotNull] ResultExecutedContext context)
        {
            _Timer.Stop();
            var process_time = _Timer.Elapsed.TotalMilliseconds;
            context.HttpContext.Response.Headers.Add("ElapsedTime", process_time.ToString(CultureInfo.InvariantCulture));
        }
    }
}