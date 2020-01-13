using System;
using System.Globalization;
using System.Linq;
using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
// ReSharper disable UnusedType.Global

namespace MathCore.ASP.Constraints.Actions
{
    /// <summary>Проверка браузера пользователя</summary>
    /// <code>
    /// public class HomeController : Controller
    /// {
    ///     public IActionResult Index() => View();
    ///
    ///     [ActionName(nameof(Index)), UserAgent("Edge")]
    ///     public IActionResult IndexEdge() => View();
    /// } 
    /// </code>
    public sealed class UserAgentAttribute : Attribute, IActionConstraint
    {
        /// <summary>Название User-Agent</summary>
        private readonly string _Name;

        /// <summary>Порядок проверки == 0</summary>
        public int Order { get; } = 0;

        /// <summary>Инициализация нового экземпляра <see cref="UserAgentAttribute"/></summary>
        /// <param name="Name">Название User-Agent</param>
        public UserAgentAttribute([NotNull] string Name) => _Name = Name.ToUpper(CultureInfo.InvariantCulture);

        /// <summary>Определение условий когда действие является корректным</summary>
        /// <param name="context">Контекст проверки <see cref="T:Microsoft.AspNetCore.Mvc.ActionConstraints.ActionConstraintContext" />.</param>
        /// <returns><see langword="true"/> - если действие корректно и <see langword="false"/>  - в противном случае.</returns>
        public bool Accept([NotNull] ActionConstraintContext context) => context
           .RouteContext
           .HttpContext
           .Request
           .Headers["User-Agent"]
           .Any(h => h.ToUpper(CultureInfo.InvariantCulture).Contains(_Name));
    }
}
