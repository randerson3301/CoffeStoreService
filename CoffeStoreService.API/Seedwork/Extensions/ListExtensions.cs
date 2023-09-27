namespace System
{
    public static class ListExtensions
    {
        public static bool HasSingleElement<T>(this IEnumerable<T> list)
        {
            return list.Count() == 1;
        }
    }
}
