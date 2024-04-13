namespace System
{
    public static class ListExtensions
    {
        public static bool ContainsRange<T>(this IEnumerable<T> source, IEnumerable<T> values)
        {
            foreach(var v in values)
            {
                if (!source.Contains(v))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
