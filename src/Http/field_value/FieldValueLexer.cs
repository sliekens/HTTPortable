using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_value
{
    public sealed class FieldValueLexer : Lexer<FieldValue>
    {
        private readonly ILexer<Repetition> innerLexer;

        public FieldValueLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<FieldValue> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<FieldValue>.FromResult(new FieldValue(result.Element));
            }
            return ReadResult<FieldValue>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
