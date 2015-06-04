namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;

    using SLANG;
    using SLANG.Core;

    public class ChunkSizeLexer : RepetitionLexer<ChunkSize, HexadecimalDigit>
    {
        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        public ChunkSizeLexer()
            : this(new HexadecimalDigitLexer())
        {
        }

        public ChunkSizeLexer(ILexer<HexadecimalDigit> hexadecimalDigitLexer)
            : base("chunk-size", 1, int.MaxValue)
        {
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
        }

        protected override ChunkSize CreateInstance(IList<HexadecimalDigit> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new ChunkSize(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out HexadecimalDigit element)
        {
            return this.hexadecimalDigitLexer.TryRead(scanner, out element);
        }
    }
}
