using System;
using Txt;
using Txt.ABNF;

namespace Http.qdtext
{
    public sealed class QuotedTextLexer : Lexer<QuotedText>
    {
        private readonly ILexer<Alternation> innerLexer;

        public QuotedTextLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<QuotedText> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<QuotedText>.FromResult(new QuotedText(result.Element));
            }
            return ReadResult<QuotedText>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
