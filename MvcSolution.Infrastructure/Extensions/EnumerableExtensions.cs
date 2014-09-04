using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSolution
{
    public static class EnumerableExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, object> text, Func<T, object> value)
        {
            return source.ToSelectList(text, value, null, null);
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, object> text, Func<T, object> value, string optionalText)
        {
            return source.ToSelectList(text, value, null, optionalText);
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, object> text, Func<T, object> value, Func<T, bool> selected)
        {
            return source.ToSelectList(text, value, selected, null);
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, object> text, Func<T, object> value, Func<T, bool> selected, string optionalText)
        {
            var items = new List<SelectListItem>();
            if (optionalText != null)
            {
                items.Add(new SelectListItem() {Text = optionalText, Value = string.Empty});
            }
            if (source == null)
            {
                return items;
            }
            foreach (var entity in source)
            {
                var item = new SelectListItem();
                item.Text = text(entity).ToString();
                item.Value = value(entity).ToString();
                if (selected != null)
                {
                    item.Selected = selected(entity);
                }
                items.Add(item);
            }
            return items;
        }

        public static string JoinString(this IEnumerable<string> values)
        {
            return JoinString(values, ",");
        }

        public static string JoinString(this IEnumerable<string> values, string split)
        {
            var result = values.Aggregate(string.Empty, (current, value) => current + (split + value));
            result = result.TrimStart(split.ToCharArray());
            return result;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
            return source;
        }

        public static List<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> key) where TKey : class
        {
            if (source == null)
            {
                return null;
            }
            var results = new List<T>();
            foreach (var item in source)
            {
                if (!results.Any(resultItem => key(resultItem) == key(item)))
                {
                    results.Add(item);
                }
            }
            return results;
        }
    }
}
