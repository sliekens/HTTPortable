using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_codings
{
    public sealed class TransferCodingCollectionLexer : Lexer<TransferCodingCollection>
    {
        private readonly ILexer<Alternation> innerLexer;

        public TransferCodingCollectionLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferCodingCollection> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferCodingCollection>.FromResult(new TransferCodingCollection(result.Element));
            }
            return
                ReadResult<TransferCodingCollection>.FromSyntaxError(
                    SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
