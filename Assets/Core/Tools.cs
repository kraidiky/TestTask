using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Tools {
    /// <summary>Вызвать делегата в отношении каждого экземпляра в последовательности любого типа.</summary>
    /// <param name="source">Перечислитель с исходными данными.</param>
    /// <param name="func">Делегат, который надо вызывать.</param>
    public static void Each<SequenceType>(this IEnumerator<SequenceType> source, Action<SequenceType> func) {
        while (source.MoveNext())
            func(source.Current);
    }
    /// <summary>Вызвать делегата в отношении каждого экземпляра в последовательности любого типа и передать ему индекс.</summary>
    /// <param name="source">Перечислитель с исходными данными.</param>
    /// <param name="func">Делегат, который надо вызывать. Второй параметр передаваемый делегату - индекс в последовательности. Отсчёт начинается со следующего элемента последовательности. В нетронутой - с начала.</param>
    public static void Each<SequenceType>(this IEnumerator<SequenceType> source, Action<SequenceType, int> func) {
        for (int i = 0; source.MoveNext(); i++)
            func(source.Current, i);
    }
    /// <summary>Вызвать делегата в отношении каждого экземпляра в последовательности любого типа.</summary>
    /// <param name="source">Перечислитель с исходными данными.</param>
    /// <param name="func">Делегат, который надо вызывать.</param>
    public static void Each<SequenceType>(this IEnumerable<SequenceType> source, Action<SequenceType> func) {
        source.GetEnumerator().Each(func);
    }
    /// <summary>Вызвать делегата в отношении каждого экземпляра в последовательности любого типа и передать ему индекс.</summary>
    /// <param name="source">Перечислитель с исходными данными.</param>
    /// <param name="func">Делегат, который надо вызывать. Второй параметр передаваемый делегату - индекс в последовательности. Отсчёт начинается со следующего элемента последовательности. В нетронутой - с начала.</param>
    public static void Each<SequenceType>(this IEnumerable<SequenceType> source, Action<SequenceType, int> func) {
        source.GetEnumerator().Each(func);
    }
    /// <summary>Приводит элементы последовательности к строке и соединяет эти строки. Лучше Aggregate потому что не падает при пустом списке</summary>
    /// <param name="source">Исходная последовательность любого типа.</param>
    /// <param name="separator">Строка разделитель, вставляемая между элементами. Чаще всего "," или "\n"</param>
    public static string ToJoinedString<SequenceType>(this IEnumerator<SequenceType> source, string separator) {
        string msg = "";
        while (source.MoveNext()) {
            msg += (msg == "" ? "" : separator) + source.Current.ToString();
        }
        return msg;
    }
    /// <summary>Приводит элементы последовательности к строке и соединяет эти строки. Лучше Aggregate потому что не падает при пустом списке</summary>
    /// <param name="source">Исходная последовательность любого типа.</param>
    /// <param name="separator">Строка разделитель, вставляемая между элементами. Чаще всего "," или "\n"</param>
    public static string ToJoinedString<SequenceType>(this IEnumerable<SequenceType> source, string separator) {
        return source.GetEnumerator().ToJoinedString(separator);
    }
}
