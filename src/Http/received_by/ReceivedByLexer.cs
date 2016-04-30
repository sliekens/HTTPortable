using System;
using Txt;
using Txt.ABNF;

namespace Http.received_by
{
    public sealed class ReceivedByLexer : Lexer<ReceivedBy>
    {
        private readonly ILexer<Alternation> innerLexer;

        public ReceivedByLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ReceivedBy> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ReceivedBy>.FromResult(new ReceivedBy(result.Element));
            }
            return ReadResult<ReceivedBy>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
