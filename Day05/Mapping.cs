namespace Day05
{
    public readonly struct Mapping
    {
        public readonly long Destination;
        public readonly long Source;
        public readonly long Length;

        private readonly long SourceStart;
        private readonly long SourceEnd;
        private readonly long Offset;

        public Mapping(long dst, long src, long len)
        {
            Destination = dst;
            Source = src;
            Length = len;

            SourceStart = Source;
            SourceEnd = Source + Length;
            Offset = Destination - Source;
        }

        public bool InRange(long value) => SourceStart <= value && value < SourceEnd;
        internal long Convert(long value) => value + Offset;
    }
}