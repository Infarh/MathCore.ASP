// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    /// <summary>Класс методов-расширений для <see cref="KeyValuePair{TKey, TValue}"/></summary>
    internal static class KeyValuePairExtensions
    {
        /// <summary>Метод-деконструктор, выделяющий ключ и значение</summary>
        /// <typeparam name="TKey">Тип ключа пары</typeparam>
        /// <typeparam name="TValue">Тип значения пары</typeparam>
        /// <param name="Pair">Деконструируемая пара значений</param>
        /// <param name="Key">Значение ключа</param>
        /// <param name="Value">Значение значения</param>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> Pair, out TKey Key, out TValue Value)
        {
            Key = Pair.Key;
            Value = Pair.Value;
        }
    }
}
