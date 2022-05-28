using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.ExtendAndHelper.Extends
{

    public static class IEnumerableExtend
    {
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                var elementValue = keySelector(element);
                if (seenKeys.Add(elementValue))
                {
                    yield return element;
                }
            }
        }


    }

    public class ListComparer<T> : IEqualityComparer<T>
    {
        public delegate bool EqualsComparer<F>(F x, F y);

        public EqualsComparer<T> equalsComparer;

        public ListComparer(EqualsComparer<T> _euqlsComparer)
        {
            this.equalsComparer = _euqlsComparer;
        }

        public bool Equals(T x, T y)
        {
            if (null != equalsComparer)
            {
                return equalsComparer(x, y);
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
