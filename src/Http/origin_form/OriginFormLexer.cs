using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.origin_form
{
    public sealed class OriginFormLexer : Lexer<OriginForm>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public OriginFormLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<OriginForm> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<OriginForm>.FromResult(new OriginForm(result.Element));
            }
            return ReadResult<OriginForm>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
