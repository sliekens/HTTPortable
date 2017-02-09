using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace Http.chunk_size
{
    public class ChunkSizeLexerFactory : RuleLexerFactory<ChunkSize>
    {
        static ChunkSizeLexerFactory()
        {
            Default = new ChunkSizeLexerFactory(Txt.ABNF.Core.HEXDIG.HexadecimalDigitLexerFactory.Default.Singleton());
        }

        public ChunkSizeLexerFactory([NotNull] ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory)
        {
            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }
            HexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
        }

        [NotNull]
        public static ChunkSizeLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HexadecimalDigit> HexadecimalDigitLexerFactory { get; set; }

        public override ILexer<ChunkSize> Create()
        {
            var innerLexer = Repetition.Create(HexadecimalDigitLexerFactory.Create(), 1, int.MaxValue);
            return new ChunkSizeLexer(innerLexer);
        }
    }
}
