using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.partial_URI
{
    public sealed class PartialUriLexer : Lexer<PartialUri>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PartialUriLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<PartialUri> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PartialUri>.FromResult(new PartialUri(result.Element));
            }
            return ReadResult<PartialUri>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
