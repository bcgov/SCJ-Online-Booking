namespace SCJ.Booking.TaskRunner.Utils
{
    public static class ListExtensions
    {
        private static readonly Random Random = new Random();

        public static void RandomizeListOrder<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
