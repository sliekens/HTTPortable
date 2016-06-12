using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.trailer_part
{
    public sealed class TrailerPartLexer : Lexer<TrailerPart>
    {
        private readonly ILexer<Repetition> innerLexer;

        public TrailerPartLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TrailerPart> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TrailerPart>.FromResult(new TrailerPart(result.Element));
            }
            return ReadResult<TrailerPart>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
