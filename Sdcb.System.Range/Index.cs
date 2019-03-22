using System.Runtime.CompilerServices;

namespace System
{
    public readonly struct Index : IEquatable<Index>
    {
        private readonly int _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
                throw new ArgumentException("Index must not be negative.", nameof(value));

            _value = fromEnd ? ~value : value;
        }

        private Index(int value)
        {
            _value = value;
        }

        public static Index Start => new Index(0);

        public static Index End => new Index(~0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromStart(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();

            return new Index(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromEnd(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();

            return new Index(~value);
        }

        public int Value => _value < 0 ? ~_value : _value;

        public bool IsFromEnd => _value < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            int offset;

            if (IsFromEnd)
                offset = length - (~_value);
            else
                offset = _value;

            return offset;
        }

        public override bool Equals(object value) => value is Index && _value == ((Index)value)._value;

        public bool Equals(Index other) => _value == other._value;

        public override int GetHashCode() => _value;

        public static implicit operator Index(int value) => FromStart(value);

        public override string ToString()
        {
            if (IsFromEnd)
                return $"^{(uint)Value}";

            return ((uint)Value).ToString();
        }
    }
}

