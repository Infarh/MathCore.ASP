using System;
using MathCore.ASP.Annotations;
// ReSharper disable UnusedType.Global

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {
        public readonly ref struct ActionData<T, TP> where T : ControllerBase
        {
            private readonly T _Controller;
            private readonly TP _Parameter;

            public ActionData(T Controller, TP Parameter)
            {
                _Controller = Controller;
                _Parameter = Parameter;
            }

            public T Do(Action<T, TP> action)
            {
                action(_Controller, _Parameter);
                return _Controller;
            }
        }

        public static ActionData<T, TP> With<T, TP>(this T controller, TP Parameter) where T : ControllerBase => 
            new ActionData<T, TP>(controller, Parameter);

        public readonly ref struct ActionData<T, TP1, TP2> where T : ControllerBase
        {
            private readonly T _Controller;
            private readonly TP1 _Parameter1;
            private readonly TP2 _Parameter2;

            public ActionData(T Controller, TP1 Parameter1, TP2 Parameter2)
            {
                _Controller = Controller;
                _Parameter1 = Parameter1;
                _Parameter2 = Parameter2;
            }

            public T Do(Action<T, TP1, TP2> action)
            {
                action(_Controller, _Parameter1, _Parameter2);
                return _Controller;
            }
        }

        public static ActionData<T, TP1, TP2> With<T, TP1, TP2>(this T controller, TP1 Parameter1, TP2 Parameter2) where T : ControllerBase =>
            new ActionData<T, TP1, TP2>(controller, Parameter1, Parameter2);

        public readonly ref struct ActionData<T, TP1, TP2, TP3> where T : ControllerBase
        {
            private readonly T _Controller;
            private readonly TP1 _Parameter1;
            private readonly TP2 _Parameter2;
            private readonly TP3 _Parameter3;

            public ActionData(T Controller, TP1 Parameter1, TP2 Parameter2, TP3 Parameter3)
            {
                _Controller = Controller;
                _Parameter1 = Parameter1;
                _Parameter2 = Parameter2;
                _Parameter3 = Parameter3;
            }

            public T Do(Action<T, TP1, TP2, TP3> action)
            {
                action(_Controller, _Parameter1, _Parameter2, _Parameter3);
                return _Controller;
            }
        }

        public static ActionData<T, TP1, TP2, TP3> With<T, TP1, TP2, TP3>(
            this T controller, 
            TP1 Parameter1, TP2 Parameter2, TP3 Parameter3) 
            where T : ControllerBase =>
            new ActionData<T, TP1, TP2, TP3>(controller, Parameter1, Parameter2, Parameter3);


        public static T WithAction<T>([NotNull] this T controller, [NotNull] Action<T> action)
            where T : ControllerBase
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            action(controller ?? throw new ArgumentNullException(nameof(controller)));
            return controller;
        }

        public static T WithAction<T, TP>([NotNull] this T controller, TP Parameter, [NotNull] Action<T, TP> action)
            where T : ControllerBase
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            action(controller ?? throw new ArgumentNullException(nameof(controller)), Parameter);
            return controller;
        }

        public static T WithAction<T, TP1, TP2>([NotNull] this T controller, TP1 Parameter1, TP2 Parameter2, [NotNull] Action<T, TP1, TP2> action)
            where T : ControllerBase
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            action(controller ?? throw new ArgumentNullException(nameof(controller)), Parameter1, Parameter2);
            return controller;
        }

        public static T WithAction<T, TP1, TP2, TP3>(
            [NotNull] this T controller, 
            TP1 Parameter1, TP2 Parameter2, TP3 Parameter3, 
            [NotNull] Action<T, TP1, TP2, TP3> action)
            where T : ControllerBase
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            action(controller ?? throw new ArgumentNullException(nameof(controller)), Parameter1, Parameter2, Parameter3);
            return controller;
        }
    }
}
