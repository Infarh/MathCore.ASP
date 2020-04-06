using System;
using MathCore.ASP.Annotations;
// ReSharper disable UnusedType.Global

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {
        public static T With<T>([NotNull] this T controller, [NotNull] Action<T> action)
            where T : ControllerBase
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            action(controller ?? throw new ArgumentNullException(nameof(controller)));
            return controller;
        }
    }
}
