using System;
using Txt;
using Txt.ABNF;

namespace Http.obs_text
{
    public sealed class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        private readonly ILexer<Terminal> innerLexer;

        public ObsoleteTextLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ObsoleteText> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ObsoleteText>.FromResult(new ObsoleteText(result.Element));
            }
            return ReadResult<ObsoleteText>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
