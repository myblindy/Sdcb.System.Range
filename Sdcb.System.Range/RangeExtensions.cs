namespace System
{
    public static class RangeExtensions
    {
        public static int get_IndexerExtension(this int[] array, Index index) =>
            index.IsFromEnd ? array[array.Length - index.Value] : array[index.Value];

        public static int get_IndexerExtension(this Span<int> span, Index index) =>
            index.IsFromEnd ? span[span.Length - index.Value] : span[index.Value];

        public static char get_IndexerExtension(this string s, Index index) =>
            index.IsFromEnd ? s[s.Length - index.Value] : s[index.Value];

        public static Span<int> get_IndexerExtension(this int[] array, Range range) =>
            array.Slice(range);

        public static Span<int> get_IndexerExtension(this Span<int> span, Range range) =>
            span.Slice(range);

        public static string get_IndexerExtension(this string s, Range range) =>
            s.Substring(range);

        public static Span<T> Slice<T>(this T[] array, Range range)
            => array.AsSpan().Slice(range);

        public static Span<T> Slice<T>(this Span<T> span, Range range)
        {
            var (start, length) = GetStartAndLength(range, span.Length);
            return span.Slice(start, length);
        }

        public static string Substring(this string s, Range range)
        {
            var (start, length) = GetStartAndLength(range, s.Length);
            return s.Substring(start, length);
        }

        private static (int start, int length) GetStartAndLength(Range range, int entityLength)
        {
            var start = range.Start.IsFromEnd ? entityLength - range.Start.Value : range.Start.Value;
            var end = range.End.IsFromEnd ? entityLength - range.End.Value : range.End.Value;
            var length = end - start;

            return (start, length);
        }
    }
}

