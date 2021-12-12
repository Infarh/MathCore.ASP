using System;
using System.Collections.Generic;
using System.Linq;
using MathCore.ASP.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
// ReSharper disable UnusedType.Global
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Identity
{
    /// <summary>Методы-расширения для <see cref="IdentityResult"/></summary>
    public static class IdentityResultExtensions
    {
        /// <summary>Сгенерировать исключение в случае неудачного результата</summary>
        /// <param name="Result">Проверяемый результат</param>
        /// <param name="Message">Сообщение, добавляемое в исключение</param>
        public static void ThrowIfNotSuccess([NotNull] this IdentityResult Result, string? Message)
        {
            if (Result.Succeeded) return;
            throw new AggregateException(Message, Result.Errors.ToExceptions());
        }

        /// <summary>Преобразовать результат в строку сообщений об ошибках, разделённых запятой</summary>
        /// <param name="Result">Преобразуемый в строку результат</param>
        /// <param name="Separator">Разделитель сообщений об ошибках</param>
        /// <returns>Строка со всеми ошибками, разделённые запятой</returns>
        [NotNull]
        public static string ToErrorString([NotNull] this IdentityResult Result, string? Separator = ", ") =>
            string.Join(Separator, Result.Errors.Select(e => e.Description));

        /// <summary>Преобразовать результат в строку сообщений об ошибках, разделённых запятой с информацией о кодах</summary>
        /// <param name="Result">Преобразуемый в строку результат</param>
        /// <param name="Separator">Разделитель сообщений об ошибках</param>
        /// <param name="CodeSeparator">Разделитель кода ошибки и её описания</param>
        /// <returns>Строка со всеми ошибками, разделённые запятой</returns>
        [NotNull]
        public static string ToErrorStringExtended([NotNull] this IdentityResult Result, string? Separator = ", ", string CodeSeparator = ":") =>
            string.Join(Separator, Result.Errors.Select(error => string.Join(CodeSeparator, error.Code, error.Description)));

        /// <summary>Преобразовать перечисление ошибок в объекты-исключения</summary>
        /// <param name="Errors">Перечисление ошибок</param>
        /// <returns>Перечисление <see cref="Exception"/> со строкой сообщения, содержащей строку сообщения ошибки</returns>
        [NotNull]
        public static IEnumerable<Exception> ToExceptions([NotNull] this IEnumerable<IdentityError> Errors) =>
            Errors.Select(error => new Exception(string.Join(":", error.Code, error.Description)));

        /// <summary>Добавить ошибки в модель</summary>
        /// <param name="ModelState">Состояние модели</param>
        /// <param name="Result">Результат, ошибки которого надо добавить в модель</param>
        public static void AddModelErrors([NotNull] this ModelStateDictionary ModelState, [NotNull] IdentityResult Result)
        {
            if (Result.Succeeded) return;
            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}