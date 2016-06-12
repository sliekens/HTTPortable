using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.header_field
{
    public sealed class HeaderFieldLexer : Lexer<HeaderField>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HeaderFieldLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HeaderField> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HeaderField>.FromResult(new HeaderField(result.Element));
            }
            return ReadResult<HeaderField>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
