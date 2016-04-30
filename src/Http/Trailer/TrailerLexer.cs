using System;
using Txt;

namespace Http.Trailer
{
    public sealed class TrailerLexer : Lexer<Trailer>
    {
        private readonly ILexer<RequiredDelimitedList> innerLexer;

        public TrailerLexer(ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Trailer> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Trailer>.FromResult(new Trailer(result.Element));
            }
            return ReadResult<Trailer>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
