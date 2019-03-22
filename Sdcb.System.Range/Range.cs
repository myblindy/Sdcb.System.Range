using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct Range : IEquatable<Range>
    {
        public Index Start { get; }
        public Index End { get; }

        public Range(Index start, Index end)
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object value)
        {
            if (value is Range)
            {
                Range r = (Range)value;
                return r.Start.Equals(Start) && r.End.Equals(End);
            }

            return false;
        }

        public bool Equals(Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        public override string ToString()
        {
            return Start + ".." + End;
        }

        public static Range StartAt(Index start) => new Range(start, Index.End);

        public static Range EndAt(Index end) => new Range(Index.Start, end);

        public static Range All => new Range(Index.Start, Index.End);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OffsetAndLength GetOffsetAndLength(int length)
        {
            int start;
            Index startIndex = Start;
            if (startIndex.IsFromEnd)
                start = length - startIndex.Value;
            else
                start = startIndex.Value;

            int end;
            Index endIndex = End;
            if (endIndex.IsFromEnd)
                end = length - endIndex.Value;
            else
                end = endIndex.Value;

            if ((uint)end > (uint)length || (uint)start > (uint)end)
                throw new ArgumentOutOfRangeException();

            return new OffsetAndLength(start, end - start);
        }

        public readonly struct OffsetAndLength
        {
            public int Offset { get; }
            public int Length { get; }

            public OffsetAndLength(int offset, int length)
            {
                Offset = offset;
                Length = length;
            }

            public void Deconstruct(out int offset, out int length)
            {
                offset = Offset;
                length = Length;
            }
        }

    }
}

