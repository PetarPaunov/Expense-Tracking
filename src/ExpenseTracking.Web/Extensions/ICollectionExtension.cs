namespace ExpenseTracking.Web.Extensions
{
    public static class ICollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}