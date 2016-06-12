using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_vchar
{
    public sealed class FieldVisibleCharacterLexer : Lexer<FieldVisibleCharacter>
    {
        private readonly ILexer<Alternation> innerLexer;

        public FieldVisibleCharacterLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<FieldVisibleCharacter> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<FieldVisibleCharacter>.FromResult(new FieldVisibleCharacter(result.Element));
            }
            return
                ReadResult<FieldVisibleCharacter>.FromSyntaxError(
                    SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
