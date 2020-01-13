using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Middleware
{
    /// <summary>Промежуточное программное обеспечение логирования ошибок</summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>Делегат, следующий в конвейере обработки входящего запроса</summary>
        private readonly RequestDelegate _Next;

        /// <summary>Логгер ошибок</summary>
        private readonly ILogger<ErrorHandlingMiddleware> _Logger;

        /// <summary>Инициализация нового экземпляра <see cref="ErrorHandlingMiddleware"/></summary>
        /// <param name="Next">Делегат, следующий в конвейере обработки входящего запроса</param>
        /// <param name="Logger">Логгер ошибок</param>
        public ErrorHandlingMiddleware(RequestDelegate Next, ILogger<ErrorHandlingMiddleware> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        /// <summary>Асинхронный метод обработки входящего запроса</summary>
        /// <param name="Context">Контекст запроса</param>
        /// <returns>Задача обработки исключений в обработчиках запроса, следующих за текущим в конвейере</returns>
        public async Task Invoke(HttpContext Context)
        {
            try
            {
                await _Next(Context);
            }
            catch (Exception Error)
            {
                HandleException(Context, Error);
                throw;
            }
        }

        /// <summary>Метод обработки ошибки ошибки</summary>
        /// <param name="Context">Контекст запроса</param>
        /// <param name="Error">Возникшее исключение</param>
        private void HandleException(HttpContext Context, Exception Error) => _Logger.LogError(Error, "Ошибка при обработке запроса {0}", Context.Request.Path);
    }
}