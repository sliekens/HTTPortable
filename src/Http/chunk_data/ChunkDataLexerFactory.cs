using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.OCTET;
using Txt.Core;

namespace Http.chunk_data
{
    public class ChunkDataLexerFactory : RuleLexerFactory<ChunkData>
    {
        static ChunkDataLexerFactory()
        {
            Default = new ChunkDataLexerFactory(OctetLexerFactory.Default.Singleton());
        }

        public ChunkDataLexerFactory([NotNull] ILexerFactory<Octet> octet)
        {
            if (octet == null)
            {
                throw new ArgumentNullException(nameof(octet));
            }
            Octet = octet;
        }

        [NotNull]
        public static ChunkDataLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Octet> Octet { get; set; }

        public override ILexer<ChunkData> Create()
        {
            var innerLexer = Repetition.Create(Octet.Create(), 1, int.MaxValue);
            return new ChunkDataLexer(innerLexer);
        }
    }
}
