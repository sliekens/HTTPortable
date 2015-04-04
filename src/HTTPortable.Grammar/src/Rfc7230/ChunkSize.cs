namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;

    using SLANG;
    using SLANG.Core;

    public class ChunkSize : Repetition<HexadecimalDigit>
    {
        public ChunkSize(IList<HexadecimalDigit> elements, ITextContext context)
            : base(elements, 1, int.MaxValue, context)
        {
        }
    }
}
