using System;
using Http.OWS;
using Txt;
using Txt.Core;

namespace Http.BWS
{
    public sealed class BadWhiteSpaceLexer : Lexer<BadWhiteSpace>
    {
        private readonly ILexer<OptionalWhiteSpace> innerLexer;

        public BadWhiteSpaceLexer(ILexer<OptionalWhiteSpace> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<BadWhiteSpace> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<BadWhiteSpace>.FromResult(new BadWhiteSpace(result.Element));
            }
            return ReadResult<BadWhiteSpace>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
