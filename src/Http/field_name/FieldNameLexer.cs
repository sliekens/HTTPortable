using System;
using Http.token;
using Txt;
using Txt.Core;

namespace Http.field_name
{
    public sealed class FieldNameLexer : Lexer<FieldName>
    {
        private readonly ILexer<Token> innerLexer;

        public FieldNameLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<FieldName> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<FieldName>.FromResult(new FieldName(result.Element));
            }
            return ReadResult<FieldName>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
