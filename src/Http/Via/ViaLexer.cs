using System;
using Txt;
using Txt.Core;

namespace Http.Via
{
    public sealed class ViaLexer : Lexer<Via>
    {
        private readonly ILexer<RequiredDelimitedList> innerLexer;

        public ViaLexer(ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Via> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Via>.FromResult(new Via(result.Element));
            }
            return ReadResult<Via>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
