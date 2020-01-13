using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace MathCore.ASP.TagHelpers
{
    /// <summary>Таг-хелпер, обеспечивающий добавление класса active к элементу, если параметры его маршрута и параметры запроса совпадают</summary>
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRouteTagHelper : TagHelper
    {
        /// <summary>Имя атрибута на которое будет реагировать хелпер</summary>
        public const string AttributeName = "is-active-route";

        /// <summary>Атрибут, указывающий, что не надо проводить проверку совпадения имени действия</summary>
        public const string IgnoreActionName = "ignore-action";

        /// <summary>Класс активации элемента</summary>
        public string ActiveClass { get; set; } = "active";

        /// <summary>Действие, указанное в элементе разметки</summary>
        [HtmlAttributeName("asp-action")]
        public string? Action { get; set; }

        /// <summary>Контроллер, указанный в элементе разметки</summary>
        [HtmlAttributeName("asp-controller")]
        public string? Controller { get; set; }

        /// <summary>Дополнительные параметры элемента разметки</summary>
        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>Контекст хелпера</summary>
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <summary>
        /// Синхронное выполнение <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper"/>
        /// с заданным <paramref name="context"/> и <paramref name="output"/>
        /// </summary>
        /// <param name="context">Содержит информацию, связанную с текущим HTML элементом</param>
        /// <param name="output">Вывод HTML-элемента</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignore_action = context.AllAttributes.TryGetAttribute(IgnoreActionName, out _);

            if (IsActive(ignore_action))
                MakeActive(output);

            output.Attributes.RemoveAll(AttributeName);
        }

        /// <summary>Проверка - является ли текущий элемент активным?</summary>
        /// <param name="IgnoreAction">Игнорировать ли проверку на совпадение действия?</param>
        /// <returns>Истина, если элемент должен быть активным</returns>
        private bool IsActive(bool IgnoreAction)
        {
            var route_values = ViewContext!.RouteData.Values;
            var current_controller = route_values["Controller"].ToString();
            var current_action = route_values["Action"].ToString();

            const StringComparison ignore_case = StringComparison.OrdinalIgnoreCase;
            if (!string.IsNullOrWhiteSpace(Controller) && !string.Equals(Controller, current_controller, ignore_case))
                return false;

            if (!IgnoreAction && !string.IsNullOrWhiteSpace(Action) && !string.Equals(Action, current_action, ignore_case))
                return false;

            foreach (var (key, value) in RouteValues)
                if (!route_values.ContainsKey(key) || route_values[key].ToString() != value)
                    return false;

            return true;
        }

        /// <summary>Сделать элемент активным</summary>
        /// <param name="output">Вывод элемента</param>
        private void MakeActive(TagHelperOutput output)
        {
            var class_attribute = output.Attributes.FirstOrDefault(a => a.Name == "class");

            if (class_attribute is null)
            {
                class_attribute = new TagHelperAttribute("class", "active");
                output.Attributes.Add(class_attribute);
            }
            else
                output.Attributes.SetAttribute(
                    "class",
                    class_attribute.Value is null
                        ? ActiveClass
                        : string.Join(" ", class_attribute.Value, ActiveClass));
        }
    }
}