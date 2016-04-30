using System;
using Txt;
using Txt.ABNF;

namespace Http.field_content
{
    public sealed class FieldContentLexer : Lexer<FieldContent>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public FieldContentLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<FieldContent> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<FieldContent>.FromResult(new FieldContent(result.Element));
            }
            return ReadResult<FieldContent>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
