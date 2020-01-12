using System;
using System.Text;
using System.Threading.Tasks;
using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Http;
// ReSharper disable UnusedType.Global
// ReSharper disable ConvertToAutoPropertyWhenPossible
// ReSharper disable UnusedMember.Global

namespace MathCore.ASP.Middleware
{
    /// <summary>
    /// Промежуточное программное обеспечение, обеспечивающее простейший вид ограничения доступа
    /// по средствам проверки имени пользователя и пароля, добавляемых в заголовки запроса
    /// </summary>
    public class AuthenticationMiddleware
    {
        /// <summary>Длина имени заголовка поля авторизации</summary>
        private static readonly int __FieldLength = "Basic ".Length;

        /// <summary>Следующий делегат в конвейере обработчиков входящего запроса</summary>
        private readonly RequestDelegate _Next;

        /// <summary>Имя пользователя</summary>
        private string _UserName = "User";

        /// <summary>Пароль</summary>
        private string _Password = "Password";

        /// <summary>Имя пользователя</summary>
        public string UserName { get => _UserName; set => _UserName = value; }

        /// <summary>Пароль</summary>
        public string Password { get => _Password; set => _Password = value; }

        /// <summary>Инициализация нового экземпляра <see cref="AuthenticationMiddleware"/></summary>
        /// <param name="Next">Следующий делегат в конвейере обработчиков входящего запроса</param>
        public AuthenticationMiddleware(RequestDelegate Next) => _Next = Next;

        /// <summary>Обработка контекста входящего запроса</summary>
        /// <param name="Context">Контекст запроса</param>
        /// <returns>Задача обработки контекста запроса</returns>
        public async Task Invoke([NotNull] HttpContext Context)
        {
            if (Context is null) throw new ArgumentNullException(nameof(Context));

            string auth_header = Context.Request.Headers["Authorization"];
            if (auth_header?.StartsWith("Basic", StringComparison.OrdinalIgnoreCase) != true) // no authorization header
            {
                Context.Response.StatusCode = 401; //Unauthorized
                return;
            }

            //Extract credentials
            var username_password = Encoding
               .GetEncoding("iso-8859-1")
               .GetString(Convert.FromBase64String(auth_header.Substring(__FieldLength).Trim()));

            if (string.IsNullOrEmpty(username_password) || username_password.StartsWith($"{_UserName}:{_Password}", StringComparison.Ordinal))
                Context.Response.StatusCode = 401; //Unauthorized
            else
                await _Next.Invoke(Context);
        }
    }
}
