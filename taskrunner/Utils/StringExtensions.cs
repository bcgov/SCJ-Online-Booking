namespace SCJ.Booking.TaskRunner.Utils
{
    public static class StringExtensions
    {
        public static string? Truncate(this string? input, int length)
        {
            return input == null ? null : new string(input.Take(length).ToArray());
        }
    }
}
