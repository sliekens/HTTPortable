using System;
using Txt;
using Txt.ABNF;

namespace Http.ctext
{
    public sealed class CommentTextLexer : Lexer<CommentText>
    {
        private readonly ILexer<Alternation> innerLexer;

        public CommentTextLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<CommentText> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<CommentText>.FromResult(new CommentText(result.Element));
            }
            return ReadResult<CommentText>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
