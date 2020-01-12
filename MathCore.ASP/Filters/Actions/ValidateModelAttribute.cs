using System.Linq;
using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MathCore.ASP.Filters.Actions
{
    /// <summary>Проверка корректности состояния модели</summary>
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        #region Properties

        /// <summary>Перенаправление на контроллер (если не указано, то на текущий)</summary>
        public string? RedirectToController { get; set; }

        /// <summary>Перенаправление на действие контроллера (если не указано, то на текущее)</summary>
        public string? RedirectToAction { get; set; }

        /// <summary>Перенаправление на страницу (если не указано, то на текущее действие контроллера)</summary>
        public string? RedirectToPage { get; set; }

        #endregion

        /// <inheritdoc />
        public override void OnActionExecuting([NotNull] ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                if (!string.IsNullOrWhiteSpace(RedirectToPage)
                   || !string.IsNullOrWhiteSpace(RedirectToController) 
                   && !string.IsNullOrWhiteSpace(RedirectToAction))
                    context.Result = new RedirectToRouteResult(ConstructRouteValueDictionary());
                else

                {
                    var controller = context.Controller as Controller;
                    var model = context.ActionArguments?.Count > 0
                        ? context.ActionArguments.First().Value
                        : null;
                    // ReSharper disable once Mvc.ViewNotResolved
                    context.Result = (IActionResult?)controller?.View(model) ?? new BadRequestResult();
                }

            base.OnActionExecuting(context);
        }

        #region Private Methods

        /// <summary>Создать словарь с данными маршрута с именами контроллера и его действия, либо с именем страницы</summary>
        /// <returns>Словарь с данными маршрута</returns>
        [NotNull]
        private RouteValueDictionary ConstructRouteValueDictionary()
        {
            var dict = new RouteValueDictionary();

            if (!string.IsNullOrWhiteSpace(RedirectToPage))
                dict.Add("page", RedirectToPage);
            else
            {
                dict.Add("controller", RedirectToController);
                dict.Add("action", RedirectToAction);
            }

            return dict;
        }

        #endregion
    }
}